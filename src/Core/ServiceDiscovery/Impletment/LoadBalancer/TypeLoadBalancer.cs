using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ServiceDiscovery.Impletment.LoadBalancer
{
    public static class TypeLoadBalancer
    {
        public static ILoadBalancer RandomLoad = new RandomLoadBalancer();
        public static ILoadBalancer RoundRobin = new RoundRobinLoadBalancer();
    }
}
