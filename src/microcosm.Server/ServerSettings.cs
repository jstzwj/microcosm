using Microcosm.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Microcosm.Server
{
    public class ServerSettings
    {
        public IPAddress LocalAddress { get; set; }

        public Int32 LocalPort { get; set; }

        public bool EnableCluster { get; set; } = true;

        public List<NodeAddress> SeedNodeAddresses { get; set; } = new List<NodeAddress>();
    }
}
