using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cap
{
    public static class CapServiceExtensions
    {
        public static IServiceCollection AddCap(this IServiceCollection services,IConfiguration configuration = null)
        {
            configuration = (configuration ?? services.BuildServiceProvider().GetService<IConfiguration>());
            CapOptions capOptions = configuration.GetSection("Cap").Get<CapOptions>();
            services.AddCap(x =>
            {
                x.UseMySql(configuration.GetSection("ConnectionStrings:MysqlUser").Value);
                x.UseRabbitMQ(configuration["EventBus:EventBusConnection"]);
                x.UseDashboard();
                x.FailedRetryCount = 5;
                x.FailedThresholdCallback = (type) =>
                {
                    Console.WriteLine(
                        $@"A message of type {type} failed after executing {x.FailedRetryCount} several times, requiring manual troubleshooting. Message name: {type.Message.Value.ToString()}");
                };
            });

            return services;
        }
    }
}
