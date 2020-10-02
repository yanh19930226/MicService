using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
	public static class DataServiceExtension
    {

		//public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration = null)
		//{
		//	configuration = (configuration ?? services.BuildServiceProvider().GetService<IConfiguration>());
		//	IConfigurationSection section = configuration.GetSection("EventBus");
		//	services.Configure<EventBusOptions>(section).AddEventBusCore();
		//	return services;
		//}


		//public static IApplicationBuilder UseEventBus(this IApplicationBuilder app, Action<IEventBus> bindAction = null)
		//{
		//	var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
		//	if (bindAction != null)
		//	{
		//		bindAction(eventBus);
		//	}
		//	return app;
		//}
	}
}
