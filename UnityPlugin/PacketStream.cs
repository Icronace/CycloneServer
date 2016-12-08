using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace De.Cyclone.Network
{
    public partial class PacketStream
    {
        private static Encoding encoding = Encoding.UTF8;

        public byte ReadUnsignedByte()
        {
            int value = Stream.ReadByte();
            if (value == -1) {
                throw new EndOfStreamException();
            }
            return (byte) value;
        }

        public void WriteUnsignedByte(byte value)
        {
            Stream.WriteByte(value);
        }

        public new sbyte ReadByte()
        {
            return (sbyte) ReadUnsignedByte();
        }

        public void WriteByte(sbyte value)
        {
            WriteUnsignedByte((byte) value);
        }

        public ushort ReadUnsignedShort()
        {
            return (ushort) ((ReadUnsignedByte() << 8) | ReadUnsignedByte());
        }

        public void WriteUnsignedShort(ushort value)
        {
            Write(new byte[] { (byte) ((value >> 8) & 0xFF), (byte) (value & 0xFF) }, 0, 2);
        }

        public short ReadShort()
        {
            return (short) ReadUnsignedShort();
        }

        public void WriteShort(short value)
        {
            WriteUnsignedShort((ushort) value);
        }

        public uint ReadUnsignedInt()
        {
            return (uint) ((ReadUnsignedByte() << 24) | (ReadUnsignedByte() << 16) | (ReadUnsignedByte() << 8) | ReadUnsignedByte());
        }

        public void WriteUnsignedInt(uint value)
        {
            Write(new byte[] { (byte) ((value >> 24) & 0xFF), (byte) ((value >> 16) & 0xFF), (byte) ((value >> 8) & 0xFF), (byte) (value & 0xFF) }, 0, 4);
        }

        public int ReadInt()
        {
            return (int) ReadUnsignedInt();
        }

        public void WriteInt(int value)
        {
            WriteUnsignedInt((uint) value);
        }

        public ulong ReadUnsignedLong()
        {
            return (ulong) ((ReadUnsignedByte() << 56) | (ReadUnsignedByte() << 48) | (ReadUnsignedByte() << 40) | (ReadUnsignedByte() << 32) | (ReadUnsignedByte() << 24) | (ReadUnsignedByte() << 16) | (ReadUnsignedByte() << 8) | ReadUnsignedByte());
        }

        public void WriteUnsignedLong(ulong value)
        {
            Write(new byte[] { (byte) ((value >> 56) & 0xFF), (byte) ((value >> 48) & 0xFF), (byte) ((value >> 40) & 0xFF), (byte) ((value >> 32) & 0xFF), (byte) ((value >> 24) & 0xFF), (byte) ((value >> 16) & 0xFF), (byte) ((value >> 8) & 0xFF), (byte) (value & 0xFF) }, 0, 4);
        }

        public long ReadLong()
        {
            return (long) ReadUnsignedLong();
        }

        public void WriteLong(long value)
        {
            WriteUnsignedLong((ulong) value);
        }

        public unsafe float ReadFloat()
        {
            uint value = ReadUnsignedInt();
            return *(float*)&value;
        }

        public unsafe void WriteFloat(float value)
        {
            WriteUnsignedInt(*(uint*)&value);
        }

        public unsafe double ReadDouble()
        {
            ulong value = ReadUnsignedLong();
            return *(double*)&value;
        }

        public unsafe void WriteDouble(double value)
        {
            WriteUnsignedLong(*(ulong*)&value);
        }

        public bool ReadBool()
        {
            return ReadUnsignedByte() != 0;
        }

        public void WriteBool(bool value)
        {
            WriteUnsignedByte(value ? (byte) 1 : (byte) 0);
        }

        public Guid ReadGuid()
        {
            var data = new byte[16];
            Read(data, 0, 16);
            return new Guid(data);
        }

        public void WriteGuid(Guid value)
        {
            Write(value.ToByteArray(), 0, 16);
        }

        public string ReadString()
        {
            var data = new byte[ReadInt()];
            Read(data, 0, data.Length);
            return encoding.GetString(data);
        }

        public void WriteString(string value)
        {
            WriteInt(encoding.GetByteCount(value));
            Write(encoding.GetBytes(value), 0, encoding.GetByteCount(value));
        }
    }
}