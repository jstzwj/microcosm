using Microcosm.Core;
using Microcosm.Core.Network;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Microcosm.Server
{
    public class Server
    {
        private List<NodeAddress> _seedNodes;

        private NodeType _nodeType;

        private NodeState _nodeState;

        private NodeAddress _localAddress;

        private ConnectionHub _hub;

        private bool _enableCluster;

        private ILoggerFactory _loggerFactory;

        public Server(ServerSettings settings)
        {
            _enableCluster = settings.EnableCluster;
            _seedNodes = settings.SeedNodeAddresses;
            _nodeType = NodeType.Worker;
            _nodeState = NodeState.Joining;

            _localAddress = new NodeAddress(GetLocalIp(), settings.LocalPort, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());

            _loggerFactory = LoggerFactory.Create(builder => {
                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("Microcosm.Server", LogLevel.Debug)
                       .AddConsole();
            });

            _hub = new ConnectionHub(settings.LocalAddress, settings.LocalPort, _loggerFactory);
        }

        public void Startup()
        {
            Task.Run(
                async () => await _hub.Startup(default(CancellationToken))
            ).Wait();
        }

        private IPAddress GetLocalIp()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(hostName);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipa;
                }
            }

            return IPAddress.Loopback;
        }
    }
}
