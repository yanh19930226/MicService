using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Core.EventBus.Impletment.RabbitMq.EventBusRabbitMQ
{
	public class DefaultRabbitMQPersistentConnection : IRabbitMQPersistentConnection, IDisposable
	{
		private readonly IConnectionFactory _connectionFactory;

		private readonly int _retryCount;

		private IConnection _connection;

		private bool _disposed;

		private object sync_root = new object();

		public bool IsConnected
		{
			get
			{
				if (_connection != null && _connection.IsOpen)
				{
					return !_disposed;
				}
				return false;
			}
		}

		public DefaultRabbitMQPersistentConnection(IConnectionFactory connectionFactory, int retryCount = 5)
		{
			_connectionFactory = (connectionFactory ?? throw new ArgumentNullException("connectionFactory"));
			_retryCount = retryCount;
		}

		public IModel CreateModel()
		{
			if (!IsConnected)
			{
				throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
			}
			return _connection.CreateModel();
		}

		public void Dispose()
		{
			if (!_disposed)
			{
				_disposed = true;
				try
				{
					_connection.Dispose();
				}
				catch (IOException)
				{
				}
			}
		}

		public bool TryConnect()
		{
			lock (sync_root)
			{
				Policy.Handle<SocketException>().Or<BrokerUnreachableException>().WaitAndRetry(_retryCount, (Func<int, TimeSpan>)((int retryAttempt) => TimeSpan.FromSeconds(Math.Pow(2.0, retryAttempt))), (Action<Exception, TimeSpan>)delegate
				{
				})
					.Execute(delegate
					{
						_connection = _connectionFactory.CreateConnection();
					});
				if (IsConnected)
				{
					_connection.ConnectionShutdown += OnConnectionShutdown;
					_connection.CallbackException += OnCallbackException;
					_connection.ConnectionBlocked += OnConnectionBlocked;
					return true;
				}
				return false;
			}
		}

		private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
		{
			if (!_disposed)
			{
				TryConnect();
			}
		}

		private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
		{
			if (!_disposed)
			{
				TryConnect();
			}
		}

		private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
		{
			if (!_disposed)
			{
				TryConnect();
			}
		}
	}
}
