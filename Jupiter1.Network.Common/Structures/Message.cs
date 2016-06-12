using System;

namespace Jupiter1.Network.Common.Structures
{
    public sealed class Message
    {
        public byte[] Data { get; set; }

        public int Offset { get; set; }

        public short ReadInt16()
        {
            var buffer = new byte[sizeof(short)];
            ReadData(buffer, 0, sizeof(short));
            return BitConverter.ToInt16(buffer, 0);
        }

        public void WriteInt16(short value)
        {
            var buffer = BitConverter.GetBytes(value);
            WriteData(buffer, 0, sizeof(short));
        }

        public ushort ReadUInt16()
        {
            var buffer = new byte[sizeof(ushort)];
            ReadData(buffer, 0, sizeof(ushort));
            return BitConverter.ToUInt16(buffer, 0);
        }

        public void WriteUInt16(ushort value)
        {
            var buffer = BitConverter.GetBytes(value);
            WriteData(buffer, 0, sizeof(ushort));
        }

        public int ReadInt32()
        {
            var buffer = new byte[sizeof(int)];
            ReadData(buffer, 0, sizeof(int));
            return BitConverter.ToInt32(buffer, 0);
        }

        public void WriteInt32(int value)
        {
            var buffer = BitConverter.GetBytes(value);
            WriteData(buffer, 0, sizeof(int));
        }

        public uint ReadUInt32()
        {
            var buffer = new byte[sizeof(uint)];
            ReadData(buffer, 0, sizeof(uint));
            return BitConverter.ToUInt32(buffer, 0);
        }

        public void WriteUInt32(uint value)
        {
            var buffer = BitConverter.GetBytes(value);
            WriteData(buffer, 0, sizeof(uint));
        }

        public void ReadData(byte[] buffer, int offset, int length)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentException(nameof(offset));

            Buffer.BlockCopy(Data, Offset, buffer, offset, length);
            Offset += length;
        }

        public void WriteData(byte[] buffer, int offset, int length)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentException(nameof(offset));
            if (length <= 0)
                throw new ArgumentException(nameof(length));

            Buffer.BlockCopy(buffer, offset, Data, Offset, length);
            Offset += length;
        }
    }
}