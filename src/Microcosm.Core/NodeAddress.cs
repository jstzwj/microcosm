using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Microcosm.Core
{
    public struct NodeAddress
    {
        public IPAddress Hostname { get; set; }

        public int Port { get; set; }

        public long StartTime { get; set; }

        public NodeAddress(IPAddress host, int port, long time)
        {
            Hostname = host;
            Port = port;
            StartTime = time;
        }
    }
}
