using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceDiscovery.Impletment.Builder
{
    public class ServiceDiscoveryBuilder : IServiceDiscoveryBuilder
    {
        public IServiceDiscoveryProvider ServiceProvider { get; set; }

        public string ServiceName { get; set; }

        public string UriScheme { get; set; }

        public ILoadBalancer LoadBalancer { get; set; }

        public ServiceDiscoveryBuilder(IServiceDiscoveryProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public async Task<Uri> BuildAsync(string path)
        {
            var serviceList = await ServiceProvider.GetServicesAsync(ServiceName);
            var service = LoadBalancer.Resolve(serviceList);
            var baseUri = new Uri($"{UriScheme}://{service}");
            var uri = new Uri(baseUri, path);
            return uri;
        }
    }
}
