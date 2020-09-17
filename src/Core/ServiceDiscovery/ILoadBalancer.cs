using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceDiscovery
{
    public interface ILoadBalancer
    {
        string Resolve(IList<string> services);
    }
}
