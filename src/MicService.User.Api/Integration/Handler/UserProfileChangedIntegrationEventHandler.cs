using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using MicService.Abstractions.IntegrationEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api.Integration.Handler
{
    public class UserProfileChangedIntegrationEventHandler : ICapSubscribe
    {
        private readonly ILogger<UserProfileChangedIntegrationEventHandler> _logger;
        public UserProfileChangedIntegrationEventHandler(ILogger<UserProfileChangedIntegrationEventHandler> logger)
        {
            _logger = logger;
        }
        [CapSubscribe("UserProfileChanged")]
        public  Task UserProfileChanged(UserProfileChangedIntegrationEvent @event)
        {
            _logger.LogInformation(@event.UserId.ToString());
            _logger.LogInformation(@event.Name);
            _logger.LogInformation(@event.Avatar);
            return Task.CompletedTask;
        }
    }
}
