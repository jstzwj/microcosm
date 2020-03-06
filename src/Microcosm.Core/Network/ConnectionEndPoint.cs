using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Microcosm.Core.Network
{
    public struct ConnectionEndPoint : IEquatable<ConnectionEndPoint>
    {
        public IPAddress Ip { get; set; }

        public Int32 Port { get; set; }

        public ConnectionEndPoint(IPAddress ip, Int32 port)
        {
            Ip = ip;
            Port = port;
        }

        public override bool Equals(object obj)
        {
            return obj is ConnectionEndPoint && Equals((ConnectionEndPoint)obj);
        }

        public bool Equals(ConnectionEndPoint other)
        {
            return Ip == other.Ip &&
                   Port == other.Port;
        }

        public override int GetHashCode()
        {
            var hashCode = -81208087;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Ip.GetHashCode();
            hashCode = hashCode * -1521134295 + Port.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(ConnectionEndPoint state1, ConnectionEndPoint state2)
        {
            return state1.Equals(state2);
        }

        public static bool operator !=(ConnectionEndPoint state1, ConnectionEndPoint state2)
        {
            return !(state1 == state2);
        }
    }
}
