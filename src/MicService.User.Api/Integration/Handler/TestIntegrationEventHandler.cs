using Core.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using MicService.User.Api.Integration.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Integration.Handler
{
    public class TestIntegrationEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        private readonly ILogger<TestIntegrationEventHandler> _logger;
        public TestIntegrationEventHandler(ILogger<TestIntegrationEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(TestIntegrationEvent @event)
        {
            _logger.LogInformation(@event.EventData.Id);
            _logger.LogInformation(@event.EventData.Title);
            return Task.CompletedTask;
        }
    }
}
