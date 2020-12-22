using Core.AutoDI;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quartz.Net.HostService.Services
{
    public class TestService : IAutoDIable
    {
        private readonly ILogger<TestService> _logger;
        public TestService(ILogger<TestService> logger)
        {
            _logger = logger;
        }
        public void Execute()
        {
            _logger.LogInformation("TestService");
        }
    }
}
