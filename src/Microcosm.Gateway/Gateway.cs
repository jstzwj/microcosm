using Microcosm.Core.Network;
using Microcosm.Core;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace Microcosm.Gateway
{
    public class Gateway
    {
        private List<NodeAddress> _seedNodes;

        private NodeAddress _localAddress;

        private ConnectionHub _hub;

        private bool _enableCluster;

        private ILoggerFactory _loggerFactory;

        public Gateway(GatewaySettings settings)
        {
            _enableCluster = settings.EnableCluster;
            _seedNodes = settings.SeedNodeAddresses;

            _localAddress = new NodeAddress(GetLocalIp(), settings.LocalPort, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());

            _loggerFactory = LoggerFactory.Create(builder => {
                builder.AddFilter("Microsoft", LogLevel.Warning)
                       .AddFilter("System", LogLevel.Warning)
                       .AddFilter("Microcosm.Server", LogLevel.Debug)
                       .AddConsole();
            });

            _hub = new ConnectionHub(settings.LocalAddress, settings.LocalPort, _loggerFactory);
        }

        public async Task Connect()
        {
            await _hub.Startup(default(CancellationToken));

            foreach (var eachNode in _seedNodes)
            {
                var session = await _hub.GetConnectionSession(eachNode.Hostname, eachNode.Port);
            }
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
