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
        public static IServiceCollection AddCap(this IServiceCollection services, DbContext context,IConfiguration configuration = null)
        {
            configuration = (configuration ?? services.BuildServiceProvider().GetService<IConfiguration>());
            CapOptions capOptions = configuration.GetSection("Cap").Get<CapOptions>();
            //services.AddCap(options =>
            //{
            //    options.UseEntityFramework<context>()
            //    .UseMySql(capOptions.CurrentNodeHostName)
            //    .UseRabbitMQ("localhost")
            //    .UseDashboard();

            //    options.UseDiscovery(opt =>
            //    {
            //        opt.DiscoveryServerHostName = capOptions.CurrentNodeHostName;
            //        opt.DiscoveryServerPort = capOptions.CurrentNodeHostName;
            //        opt.CurrentNodeHostName = capOptions.CurrentNodeHostName;
            //        opt.CurrentNodePort = 5800;
            //        opt.NodeId = capOptions.CurrentNodeHostName;
            //        opt.NodeName = capOptions.CurrentNodeHostName;
            //    });
            //});
            return services;
        }
    }
}
