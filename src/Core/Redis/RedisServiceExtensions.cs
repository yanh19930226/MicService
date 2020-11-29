using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Redis
{
    public static class RedisServiceExtensions
    {
        public static IServiceCollection AddCoreSwagger(this IServiceCollection services, IConfiguration configuration = null)
        {
            configuration = (configuration ?? services.BuildServiceProvider().GetService<IConfiguration>());
            RedisOptions redisOption = configuration.GetSection("Redis").Get<RedisOptions>();

            //连接字符串
            string Connection = redisOption.Connection;
            //实例名称
            string InstanceName = redisOption.InstanceName;
            //默认数据库 
            int DefaultDB = int.Parse(redisOption.DefaultDB ?? "0");

            services.AddSingleton(new RedisClient(Connection, InstanceName, DefaultDB));

            return services;
        }
    }
}
