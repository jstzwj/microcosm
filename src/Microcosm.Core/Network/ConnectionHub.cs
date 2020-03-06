using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microcosm.Core.Network
{
    public class ConnectionHub
    {
        private IPAddress _localAddress;

        private Int32 _listenPort;

        private readonly Dictionary<IPEndPoint, ConnectionSession> _connections;

        private readonly TcpListener _tcpListener;

        private readonly ILogger _logger;

        private CancellationToken _cancellationToken;

        public ConnectionHub(IPAddress localIp, Int32 listenPort, ILoggerFactory loggerFactory)
        {
            _localAddress = localIp;
            _listenPort = listenPort;

            _connections = new Dictionary<IPEndPoint, ConnectionSession>();
            _tcpListener = new TcpListener(localIp, listenPort);

            _logger = loggerFactory.CreateLogger<ConnectionHub>();
        }

        public async Task Startup(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            try
            {
                _tcpListener.Start();
                _logger.LogInformation("ConnectionRouter started.");
                while (!cancellationToken.IsCancellationRequested)
                {
                    DispatchIncomingClient(await _tcpListener.AcceptTcpClientAsync(), cancellationToken);
                }
                _tcpListener.Stop();
            }
            catch (FormatException)
            {
                _logger.LogError($"The configuration of gateway have an incorrect format.");
            }
        }

        public async Task<ConnectionSession> GetConnectionSession(IPAddress ip, Int32 port)
        {
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            if (_connections.ContainsKey(endPoint))
            {
                return _connections[endPoint];
            }
            else
            {
                await CreateConnection(endPoint);
                return _connections[endPoint];
            }
        }

        private async Task CreateConnection(IPEndPoint endPoint)
        {
            TcpClient client = new TcpClient();
            client.Connect(endPoint);

            var session = new ConnectionSession(client);
            await session.Startup(_cancellationToken);

            _connections.Add(endPoint, session);
        }

        private async void DispatchIncomingClient(TcpClient client, CancellationToken cancellationToken)
        {
            var remoteEndPoint = (IPEndPoint)client.Client.RemoteEndPoint;

            var session = new ConnectionSession(client);
            await session.Startup(cancellationToken);

            _connections.Add(remoteEndPoint, session);
        }
    }
}
