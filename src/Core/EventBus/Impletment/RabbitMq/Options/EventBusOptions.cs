using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EventBus.Impletment.RabbitMq.Options
{
	public class EventBusOptions
	{
		public string EventBusConnection
		{
			get;
			set;
		}

		public string EventBusUserName
		{
			get;
			set;
		}

		public string EventBusPassword
		{
			get;
			set;
		}

		public int EventBusRetryCount
		{
			get;
			set;
		}

		public string SubscriptionClientName
		{
			get;
			set;
		}

		public string ExchangeName
		{
			get;
			set;
		}
	}
}
