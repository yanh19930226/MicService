using Core.AutoDI;
using Microsoft.Extensions.Logging;
using Quartz.Net.HostService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Net.HostService.Jobs
{
    [DisallowConcurrentExecution]
    public class AdvertiseJob : IJob, IAutoDIable
    {
        private readonly ILogger<AdvertiseJob> _logger;
        public readonly TestService _service;
        public AdvertiseJob(ILogger<AdvertiseJob> logger,TestService service)
        {
            _logger = logger;
            _service = service;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("AdvertiseJob  ");
            _service.Execute();
            return Task.CompletedTask;
        }
    }
}
