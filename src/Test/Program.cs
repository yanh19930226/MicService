using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        public static async Task  Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostContext, services) =>
              {
                    services.AddHostedService<TimedHostedService>();
                  
                })
              .Build();

            await host.StartAsync();

            await host.WaitForShutdownAsync();
        }
    }
}
