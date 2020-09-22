using Core.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.EventBus.Impletment.RabbitMq.EventBusRabbitMQ
{
	public interface IIntegrationEventService
	{
		Task PublishThroughEventBusAsync(IntegrationEvent evt);
	}
}
