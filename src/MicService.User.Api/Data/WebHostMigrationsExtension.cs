using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicService.User.Api
{
    public static class WebHostMigrationsExtension
    {
        /// <summary>
        /// 初始化database方法
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="host"></param>
        /// <param name="sedder"></param>
        /// <returns></returns>
        public static IHost MigrateDbContext<TContext>(this IHost host, Action<TContext, IServiceProvider> sedder)
            where TContext : UserContext
        {
            //创建数据库实例在本区域有效
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                try
                {
                    context.Database.Migrate();//初始化database
                    sedder(context, services);
                    logger.LogInformation($"执行DbContext{typeof(TContext).Name} seed 成功");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"执行dbcontext {typeof(TContext).Name}  seed失败");
                }
            }
            return host;
        }
    }
}
