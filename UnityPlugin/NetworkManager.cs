using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace De.Cyclone.Network
{
    public class NetworkManager
    {
        private static readonly Type[] HandshakePackets;
        private static readonly Type[] SystemPackets;
        private static readonly Type[] ControlledPackets;
        private static readonly Type[] ControllingPackets;
        public Protocol Protocol { get; private set; }
        private object streamLock = new object();
        private BufferedStream BufferedStream { get; set; }
        private PacketStream PacketStream { get; set; }
        public Stream Stream
        {
            get { return Stream; }
            set
            {
                lock (streamLock) {
                    if (BufferedStream != null) {
                        BufferedStream.Flush();
                    }
                    Stream = value;
                    BufferedStream = new BufferedStream(Stream);
                    PacketStream = new PacketStream(BufferedStream);
                }
            }
        }

        static NetworkManager()
        {
            var handshakePackets = new List<Type>();
            handshakePackets.Add(typeof(HandshakePacket));
            handshakePackets.Add(typeof(AuthenticationRequestPacket));
            handshakePackets.Add(typeof(AuthenticationResponsePacket));
            handshakePackets.Add(typeof(AuthenticationFinalPacket));
            HandshakePackets = handshakePackets.ToArray();

            var systemPackets = new List<Type>();
            SystemPackets = systemPackets.ToArray();

            var controlledPackets = new List<Type>();
            ControlledPackets = controlledPackets.ToArray();

            var controllingPackets = new List<Type>();
            ControllingPackets = controllingPackets.ToArray();
        }

        public Packet ReadPacket()
        {
            lock (streamLock) {
                int length = PacketStream.ReadInt();
                byte id = PacketStream.ReadUnsignedByte();
                var data = new byte[PacketStream.ReadInt()];
                return null;
            }
        }

        public void WritePacket(Packet packet)
        {
            lock (streamLock) {

            }
        }
    }
}