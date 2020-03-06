using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Microcosm.Core.Protocol
{
    public class Packet : IPacket
    {
        public int PacketId { get; set; }

        public int VersionNum { get; set; }

        public byte[] Payload { get; set; }

        public Task DeserializeAsync(Stream stream)
        {
            using (var br = new BinaryReader(stream, Encoding.UTF8, true))
            {
                int length = br.ReadInt32();
                PacketId = br.ReadInt32();
                VersionNum = br.ReadInt32();
                Payload = br.ReadBytes(length - sizeof(int) - sizeof(int));
            }

            return Task.CompletedTask;
        }

        public Task SerializeAsync(Stream stream)
        {
            using (var bw = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                bw.Write(sizeof(int) + sizeof(int) + Payload.Length);
                bw.Write(PacketId);
                bw.Write(VersionNum);
                bw.Write(Payload);
                bw.Flush();
            }

            return Task.CompletedTask;
        }
    }


    public interface IPacket
    {
        Task SerializeAsync(Stream stream);

        Task DeserializeAsync(Stream stream);
    }

    public interface ISubPacket
    {
        Task SerializeAsync(BinaryWriter bw);

        Task DeserializeAsync(BinaryReader br);
    }
}
