using Core.EventBus.Events;
using MicService.Abstractions.IntegrationEvents.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Integration.Event
{
    public class TestIntegrationEvent : IntegrationEvent<TestIntegrationEventModel>
    {
        public TestIntegrationEvent(TestIntegrationEventModel eventData) : base(eventData)
        {

        }
    }
}
