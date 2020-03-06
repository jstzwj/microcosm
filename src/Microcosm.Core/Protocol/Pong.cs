using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Microcosm.Core.Protocol
{
    public class Pong : ISubPacket
    {
        public int Num { get; set; }

        public Task DeserializeAsync(BinaryReader br)
        {
            Num = br.ReadInt32();
            return Task.CompletedTask;
        }

        public Task SerializeAsync(BinaryWriter bw)
        {
            bw.Write(Num);
            return Task.CompletedTask;
        }
    }
}
