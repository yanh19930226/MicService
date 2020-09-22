using Core.EventBus.Abstractions;
using Core.EventBus.Events;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventBus.Infrastructure
{
	public class EventHandler<TEventHandler, TEvent> : IIntegrationEventHandler<TEvent>, IIntegrationEventHandler where TEvent : IntegrationEvent
	{
		public IStringLocalizer<TEventHandler> G
		{
			get;
			set;
		}

		public ILogger<TEventHandler> Logger
		{
			get;
			set;
		}

		public virtual async Task Handle(TEvent @event)
		{
			Logger.LogInformation("{@p}", @event);
		}
	}
}
