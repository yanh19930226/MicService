using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceDiscovery
{
    public interface IServiceDiscoveryBuilder
    {
        // 服务提供者！
        IServiceDiscoveryProvider ServiceProvider { get; set; }

        // 服务名称!
        string ServiceName { get; set; }

        // Uri方案
        string UriScheme { get; set; }

        // 你用哪种策略？
        ILoadBalancer LoadBalancer { get; set; }

        Task<Uri> BuildAsync(string path);
    }
}
