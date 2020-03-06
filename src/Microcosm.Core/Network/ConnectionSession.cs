using Microcosm.Core.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Microcosm.Core.Network
{
    public class ConnectionSession : IDisposable
    {
        private readonly TcpClient _tcpClient;
        private readonly ActionBlock<Packet> _outcomingPacketDispatcher;
        private Stream _remoteStream;

        public ConnectionSession(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _outcomingPacketDispatcher = new ActionBlock<Packet>(SendOutcomingPacket);
        }

        public async Task Startup(CancellationToken cancellationToken)
        {
            using (_remoteStream = _tcpClient.GetStream())
            {
                try
                {
                    while (!cancellationToken.IsCancellationRequested &&
                        !_outcomingPacketDispatcher.Completion.IsCompleted)
                    {
                        await DispatchIncomingPacket();
                    }
                }
                catch (EndOfStreamException)
                {
                    await _outcomingPacketDispatcher.Completion;
                }
            }
        }

        private void OnClosed()
        {
            _outcomingPacketDispatcher.Post(null);
        }

        private async Task DispatchIncomingPacket()
        {
            var packet = new Packet();
            await packet.DeserializeAsync(_remoteStream);

        }

        private async Task SendOutcomingPacket(Packet packet)
        {
            if (packet == null)
            {
                _tcpClient.Client.Shutdown(SocketShutdown.Send);
                _outcomingPacketDispatcher.Complete();
            }
            else
            {
                await packet.SerializeAsync(_remoteStream);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tcpClient.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
