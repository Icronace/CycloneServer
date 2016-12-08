using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.Cyclone.Network
{
    public interface Packet
    {
        void Read(PacketStream stream);

        void Write(PacketStream stream);
    }

    public struct HandshakePacket : Packet
    {
        public Guid Id;
        public String Host;
        public ushort Port;
        public bool Reconnect;

        public HandshakePacket(Guid id, String host, ushort port, bool reconnect)
        {
            Id = id;
            Host = host;
            Port = port;
            Reconnect = reconnect;
        }

        public void Read(PacketStream stream)
        {
            Id = stream.ReadGuid();
            Host = stream.ReadString();
            Port = stream.ReadUnsignedShort();
            Reconnect = stream.ReadBool();
        }

        public void Write(PacketStream stream)
        {
            stream.WriteGuid(Id);
            stream.WriteString(Host);
            stream.WriteUnsignedShort(Port);
            stream.WriteBool(Reconnect);
        }
    }

    public struct AuthenticationRequestPacket : Packet
    {
        public byte[] Challenge;

        public AuthenticationRequestPacket(byte[] challenge)
        {
            Challenge = challenge;
        }

        public void Read(PacketStream stream)
        {
            var modulus = new byte[stream.ReadUnsignedInt()];
            stream.Read(modulus, 0, modulus.Length);
            var exponent = new byte[stream.ReadUnsignedInt()];
            stream.Read(exponent, 0, exponent.Length);
            var signature = new byte[stream.ReadUnsignedInt()];
            stream.Read(signature, 0, signature.Length);
            Challenge = new byte[stream.ReadUnsignedInt()];
            stream.Read(Challenge, 0, Challenge.Length);
        }

        public void Write(PacketStream stream)
        {
            stream.Write(Challenge, 0, Challenge.Length);
        }
    }

    public struct AuthenticationResponsePacket : Packet
    {
        public byte[] Salt;
        public byte[] Challenge;

        public AuthenticationResponsePacket(byte[] salt, byte[] challenge)
        {
            Salt = salt;
            Challenge = challenge;
        }

        public void Read(PacketStream stream)
        {
            Salt = new byte[stream.ReadUnsignedInt()];
            stream.Read(Salt, 0, Salt.Length);
            Challenge = new byte[stream.ReadUnsignedInt()];
            stream.Read(Challenge, 0, Challenge.Length);
        }

        public void Write(PacketStream stream)
        {
            stream.Write(Salt, 0, Salt.Length);
            stream.Write(Challenge, 0, Challenge.Length);
        }
    }

    public struct AuthenticationFinalPacket : Packet
    {
        public void Read(PacketStream stream)
        {

        }

        public void Write(PacketStream stream)
        {

        }
    }
}