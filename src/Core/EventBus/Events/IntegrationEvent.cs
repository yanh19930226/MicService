using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EventBus.Events
{
	public class IntegrationEvent
	{
		[JsonProperty]
		public Guid Id
		{
			get;
			private set;
		}

		[JsonProperty]
		public DateTime CreationDate
		{
			get;
			private set;
		}

		public IntegrationEvent()
		{
			Id = Guid.NewGuid();
			CreationDate = DateTime.UtcNow;
		}

		[JsonConstructor]
		public IntegrationEvent(Guid id, DateTime createDate)
		{
			Id = id;
			CreationDate = createDate;
		}
	}
	public class IntegrationEvent<T> : IntegrationEvent
	{
		[JsonProperty]
		public T EventData
		{
			get;
			private set;
		}

		public IntegrationEvent(T eventData)
		{
			EventData = eventData;
		}

		[JsonConstructor]
		public IntegrationEvent(Guid id, DateTime createDate, T eventData)
			: base(id, createDate)
		{
			EventData = eventData;
		}
	}
}
