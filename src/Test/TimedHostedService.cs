using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public class TimedHostedService : IHostedService, IDisposable
    {

        private readonly ILogger _logger;
        private Timer _timer;
        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting." + DateTime.Now);
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));//立即执行一次,每5秒执行一次
            return Task.CompletedTask;
        }


        private void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working." + DateTime.Now);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping." + DateTime.Now);
            _timer?.Change(Timeout.Infinite, 0);//不再执行
            return Task.CompletedTask;
        }


        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
