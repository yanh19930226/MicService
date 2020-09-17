using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Consul
{
    public static class ConsulServiceExtensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.Configure<ConsulOptions>(configuration.GetSection("Consul"));
            return services;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            // 获取主机生命周期管理接口
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            // 获取服务配置项
            var serviceOptions = app.ApplicationServices.GetRequiredService<IOptions<ConsulOptions>>().Value;

            var config = app.ApplicationServices.GetRequiredService<IConfiguration>();

            var consulClient = new ConsulClient(configuration =>
            {
                //服务注册的地址，集群中任意一个地址
                configuration.Address = new Uri(serviceOptions.ConsulAddress);
            });

            // 使用参数配置服务注册地址
            var address = config["address"];
            if (string.IsNullOrEmpty(address))
            {
                // 获取当前服务地址和端口
                var features = app.Properties["server.Features"] as FeatureCollection;
                address = features?.Get<IServerAddressesFeature>().Addresses.First();
            }
            var uri = new Uri(address);

            // 节点服务注册对象
            var registration = new AgentServiceRegistration()
            {
                // 服务ID必须保证唯一
                ID = Guid.NewGuid().ToString(),
                Name = serviceOptions.ServiceName,// 服务名
                Address = uri.Host,
                Port = uri.Port, // 服务端口
                Check = new AgentServiceCheck
                {
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 健康检查地址
                    //HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceOptions.HealthCheck}",
                    HTTP = new Uri(uri, "HealthCheck").OriginalString,
                    // 健康检查时间间隔
                    Interval = TimeSpan.FromSeconds(10),
                }
            };
            //启动的时候注册服务
            lifetime.ApplicationStarted.Register(() =>
            {
                // 注册服务
                consulClient.Agent.ServiceRegister(registration).Wait();
            });

            // 注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            return app;
        }
    }
}
