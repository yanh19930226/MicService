using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Consul
{
    public class ConsulOptions
    {
        // 服务注册地址（Consul的地址，如果是集群，取任意一个地址即可）
        public string ConsulAddress { get; set; }
        // 服务名称
        public string ServiceName { get; set; }
    }

}
