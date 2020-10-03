using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data.Domain.Events
{
	public abstract class Message<T> : IRequest<T>, IBaseRequest
	{
		public string MessageType
		{
			get;
			protected set;
		}

		public long AggregateId
		{
			get;
			protected set;
		}

		protected Message()
		{
			MessageType = GetType().Name;
		}
	}
	public abstract class Message : Message<bool>
	{
	}
}
