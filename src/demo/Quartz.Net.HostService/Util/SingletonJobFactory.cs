using Core.AutoDI;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quartz.Net.HostService.Util
{
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {

            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job) { }
    }
}
