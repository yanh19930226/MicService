using Autofac;
using Core.EventBus.Abstractions;
using Core.EventBus.Impletment.RabbitMq.EventBusRabbitMQ;
using Core.EventBus.Impletment.RabbitMq.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.EventBus.Impletment.RabbitMq
{
	public static class RabbitMqServiceExtensions
    {

		public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration = null)
		{
			configuration = (configuration ?? services.BuildServiceProvider().GetService<IConfiguration>());
			IConfigurationSection section = configuration.GetSection("EventBus");
			services.Configure<EventBusOptions>(section).AddEventBusCore();
			return services;
		}

		private static void AddEventBusCore(this IServiceCollection services)
		{
			ServiceProvider provider = services.BuildServiceProvider();
			EventBusOptions EventBusOptions = provider.GetService<IOptions<EventBusOptions>>().Value;


			//Create Connection
			services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
			{
				var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
				var factory = new ConnectionFactory()
				{
					HostName = EventBusOptions.EventBusConnection,
					DispatchConsumersAsync = true
				};

				if (!string.IsNullOrEmpty(EventBusOptions.EventBusUserName))
				{
					factory.UserName = EventBusOptions.EventBusUserName;
				}

				if (!string.IsNullOrEmpty(EventBusOptions.EventBusPassword))
				{
					factory.Password =EventBusOptions.EventBusPassword;
				}
				int eventBusRetryCount =EventBusOptions.EventBusRetryCount;

				return new DefaultRabbitMQPersistentConnection(factory, eventBusRetryCount);
			});

			string subscriptionClientName = EventBusOptions.SubscriptionClientName;
			services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
			{
				var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
				var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
				var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();

				var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

				var retryCount = EventBusOptions.EventBusRetryCount;
				return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, EventBusOptions.ExchangeName, subscriptionClientName, retryCount);
			});

			services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

			//foreach IIntegrationEventHandler
			foreach (Type serviceType in Assembly.GetEntryAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IIntegrationEventHandler))).ToArray())
			{
				services.AddTransient(serviceType);
			}
		}

		public static IApplicationBuilder UseEventBus(this IApplicationBuilder app, Action<IEventBus> bindAction = null)
		{
			var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
			if (bindAction != null)
			{
				bindAction(eventBus);
			}
			return app;
		}
	}
}
