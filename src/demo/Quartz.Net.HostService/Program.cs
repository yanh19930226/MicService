using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz.Net.HostService.Jobs;
using Quartz.Net.HostService.Util;
using System;
using System.Threading.Tasks;

namespace Quartz.Net.HostService
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Time Job Start:{DateTime.Now}");

            using var host = Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddConfig()
                              .AddSingleton(new JobSchedule(typeof(AdvertiseJob), (SimpleScheduleBuilder x) => x.WithIntervalInSeconds(5).RepeatForever(), null, true));
              })
              .Build();

            await host.StartAsync();

            await host.WaitForShutdownAsync();
        }
    }
}
