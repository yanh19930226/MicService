using Core.AutoDI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz.Impl;
using Quartz.Net.HostService.Util;
using Quartz.Spi;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Quartz.Net.HostService
{
    public static class SevicesExtension
    {

        public static IServiceCollection AddConfig(this IServiceCollection services)
        {
            #region 自定义注入方法的使用
            //services.Register(typeof(IJob), Assembly.GetEntryAssembly(), ServiceLifetime.Singleton);
            //services.AddLogging()
            //             .AddSingleton<IJobFactory, SingletonJobFactory>()
            //             .AddSingleton<ISchedulerFactory, StdSchedulerFactory>()
            //             .AddSeriLog()
            //             .AddHostedService<QuartzHostedService>();
            #endregion

            #region 设置注入选项自动注入
            // Action<InjectionOption>[] arrAction = new Action<InjectionOption>[] 
            // {
            //     opt1=> { opt1.LibPrefix = "";opt1.MatchNames = new string[] { };opt1.Lifetime = ServiceLifetime.Scoped; },
            //     opt2 => { opt2.LibPrefix = ""; opt2.MatchNames = new string[] { };opt2.Lifetime = ServiceLifetime.Scoped; },
            // };
            //services.AutoDI(arrAction); 
            #endregion

            services.AutoDI();
            services.AddLogging()
                         .AddSingleton<IJobFactory, SingletonJobFactory>()
                         .AddSingleton<ISchedulerFactory, StdSchedulerFactory>()
                         .AddSeriLog()
                         .AddHostedService<QuartzHostedService>();

            return services;

        }

        public static IServiceCollection AddSeriLog(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "log.txt");

                string logTemplete = "[{Timestamp:HH:mm:ss}][{Level}]{NewLine}Source:{SourceContext}{NewLine}Message:{Message}{NewLine}{Exception}{NewLine}";

                var LoggerConfiguration = new LoggerConfiguration();

                LoggerConfiguration = LoggerConfiguration
                                                   .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                                                   .MinimumLevel.Override("System", LogEventLevel.Information);

                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File(path, LogEventLevel.Error, logTemplete,
                 rollingInterval: RollingInterval.Day,
                 rollOnFileSizeLimit: true)
                 .CreateLogger();
                return Log.Logger;
            });

            services.AddSingleton((Func<IServiceProvider, ILoggerFactory>)((IServiceProvider provider) => new Serilog.Extensions.Logging.SerilogLoggerFactory(provider.GetService<Serilog.ILogger>())));

            return services;
        }

    }
}
