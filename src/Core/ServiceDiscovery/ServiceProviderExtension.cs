using Core.ServiceDiscovery.Impletment.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceDiscovery
{
    public static class ServiceProviderExtension
    {
        public static IServiceDiscoveryBuilder CreateServiceBuilder(this IServiceDiscoveryProvider serviceProvider, Action<IServiceDiscoveryBuilder> config)
        {

            var builder = new ServiceDiscoveryBuilder(serviceProvider);
            config(builder);
            return builder;
        }
    }
}
