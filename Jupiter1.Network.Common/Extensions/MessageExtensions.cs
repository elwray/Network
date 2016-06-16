using System;
using Jupiter1.Network.Common.Structures;

namespace Jupiter1.Network.Common.Extensions
{
    public static class MessageExtensions
    {
        public static byte ReadByte(this Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return message.Data[message.Length++];
        }

        public static short ReadInt16(this Message message)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            var buffer = new byte[sizeof(short)];
            message.ReadData(buffer, 0, sizeof(short));
            return BitConverter.ToInt16(buffer, 0);
        }

        public static ushort ReadUInt16(this Message message)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            var buffer = new byte[sizeof(ushort)];
            message.ReadData(buffer, 0, sizeof(ushort));
            return BitConverter.ToUInt16(buffer, 0);
        }

        public static int ReadInt32(this Message message)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            var buffer = new byte[sizeof(int)];
            message.ReadData(buffer, 0, sizeof(int));
            return BitConverter.ToInt32(buffer, 0);
        }

        public static uint ReadUInt32(this Message message)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            var buffer = new byte[sizeof(uint)];
            message.ReadData(buffer, 0, sizeof(uint));
            return BitConverter.ToUInt32(buffer, 0);
        }

        public static void ReadData(this Message message, byte[] buffer, int offset, int length)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentException(nameof(offset));

            Buffer.BlockCopy(message.Data, message.Length, buffer, offset, length);
            message.Length += length;
        }

        public static void WriteByte(this Message message, byte value)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            message.Data[message.Length++] = value;
        }

        public static void WriteInt16(this Message message, short value)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            var buffer = BitConverter.GetBytes(value);
            message.WriteData(buffer, 0, sizeof(short));
        }

        public static void WriteUInt16(this Message message, ushort value)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            var buffer = BitConverter.GetBytes(value);
            message.WriteData(buffer, 0, sizeof(ushort));
        }

        public static void WriteInt32(this Message message, int value)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            var buffer = BitConverter.GetBytes(value);
            message.WriteData(buffer, 0, sizeof(int));
        }

        public static void WriteUInt32(this Message message, uint value)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            var buffer = BitConverter.GetBytes(value);
            message.WriteData(buffer, 0, sizeof(uint));
        }

        public static void WriteData(this Message message, byte[] buffer, int offset, int length)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentException(nameof(offset));
            if (length <= 0)
                throw new ArgumentException(nameof(length));

            Buffer.BlockCopy(buffer, offset, message.Data, message.Length, length);
            message.Length += length;
        }

        public static void WriteData(this Message message, byte[] buffer, int length)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));

            message.WriteData(buffer, 0, length);
        }

        public static void RewriteInt32(this Message message, int destinationOffset, int value)
        {
            if (message == null)
                throw new NullReferenceException(nameof(message));
            if (destinationOffset < 0)
                throw new ArgumentException(nameof(destinationOffset));

            var bytes = BitConverter.GetBytes(value);
            Buffer.BlockCopy(bytes, 0, message.Data, destinationOffset, sizeof(int));
        }
    }
}