using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cap
{
    public class CapOptions
    {
        public string DiscoveryServerHostName { get; set; }
        public string DiscoveryServerPort { get; set; }

        public string CurrentNodeHostName { get; set; }

        public string CurrentNodePort { get; set; }

        public string NodeId { get; set; }

        public string NodeName { get; set; }
    }
}
