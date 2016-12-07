using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace De.Cyclone.Network
{
    public interface Packet
    {
        void Read(PacketBuffer buffer);

        void Write(PacketBuffer buffer);
    }

    public struct HandshakePacket : Packet
    {
        public String Host;
        public ushort Port;
        public bool Reconnect;

        public HandshakePacket(String host, ushort port, bool reconnect)
        {
            Host = host;
            Port = port;
            Reconnect = reconnect;
        }

        public void Read(PacketBuffer buffer)
        {
            Host = buffer.ReadString();
            Port = buffer.ReadUnsignedShort();
        }

        public void Write(PacketBuffer buffer)
        {
            buffer.WriteString(Host);
            buffer.WriteUnsignedShort(Port);
        }
    }

    public struct EncryptionRequestPacket : Packet
    {
        // Modulus, Exponent, Signature
        public byte[] Challenge;

        public EncryptionRequestPacket(byte[] challenge)
        {
            Challenge = challenge;
        }

        public void Read(PacketBuffer buffer)
        {
            Challenge = new byte[buffer.ReadUnsignedInt()];
            buffer.Read(Challenge, 0, Challenge.Length);
        }

        public void Write(PacketBuffer buffer)
        {
            buffer.Write(Challenge, 0, Challenge.Length);
        }
    }

    public struct EncryptionResponsePacket : Packet
    {
        public byte[] SharedSecret;
        public byte[] Challenge;

        public EncryptionResponsePacket(byte[] sharedSecret, byte[] challenge)
        {
            SharedSecret = sharedSecret;
            Challenge = challenge;
        }

        public void Read(PacketBuffer buffer)
        {
            SharedSecret = new byte[buffer.ReadUnsignedInt()];
            buffer.Read(SharedSecret, 0, SharedSecret.Length);
            Challenge = new byte[buffer.ReadUnsignedInt()];
            buffer.Read(Challenge, 0, Challenge.Length);
        }

        public void Write(PacketBuffer buffer)
        {
            buffer.Write(SharedSecret, 0, SharedSecret.Length);
            buffer.Write(Challenge, 0, Challenge.Length);
        }
    }

    public struct AuthenticationPacket : Packet
    {
        public void Read(PacketBuffer buffer)
        {

        }

        public void Write(PacketBuffer buffer)
        {

        }
    }
}