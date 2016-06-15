﻿using Jupiter1.Network.Common.Extensions;
using Jupiter1.Network.Common.Structures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jupiter1.Network.Tests.Common.Extensions
{
    [TestClass]
    public class MessageExtensionsTests
    {
        private Message _message1;
        private Message _message2;

        [TestInitialize]
        public void BeforeEachMethod()
        {
            _message1 = new Message { Data = new byte[]{ 0xDA, 0xE2, 0x51, 0x81, 0x48, 0xA6 } };
            _message2 = new Message { Data = new byte[6] };
        }

        [TestMethod, TestCategory("Unit")]
        public void ReadInt16ShouldWork()
        {
            var value = _message1.ReadInt16();
            Assert.AreEqual(-7462, value);
            Assert.AreEqual(2, _message1.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void ReadUInt16ShouldWork()
        {
            var value = _message1.ReadUInt16();
            Assert.AreEqual(58074, value);
            Assert.AreEqual(2, _message1.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void ReadInt32ShouldWork()
        {
            var value = _message1.ReadInt32();
            Assert.AreEqual(-2125339942, value);
            Assert.AreEqual(4, _message1.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void ReadUInt32ShouldWork()
        {
            var value = _message1.ReadUInt32();
            Assert.AreEqual(2169627354, value);
            Assert.AreEqual(4, _message1.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void ReadDataShouldWork()
        {
            var actual = new byte[_message1.Data.Length];
            _message1.ReadData(actual, 0, _message1.Data.Length);
            CollectionAssert.AreEqual(new byte[] { 0xDA, 0xE2, 0x51, 0x81, 0x48, 0xA6 }, actual);
            Assert.AreEqual(6, _message1.Data.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void WriteInt16ShouldWork()
        {
            _message2.WriteInt16(-10293);
            CollectionAssert.AreEqual(new byte[] { 0xCB, 0xD7, 0x00, 0x00, 0x00, 0x00 }, _message2.Data);
            Assert.AreEqual(2, _message2.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void WriteUInt16ShouldWork()
        {
            _message2.WriteUInt16(26603);
            CollectionAssert.AreEqual(new byte[] { 0xEB, 0x67, 0x00, 0x00, 0x00, 0x00 }, _message2.Data);
            Assert.AreEqual(2, _message2.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void WriteInt32ShouldWork()
        {
            _message2.WriteInt32(-661766);
            CollectionAssert.AreEqual(new byte[] { 0xFA, 0xE6, 0xF5, 0xFF, 0x00, 0x00 }, _message2.Data);
            Assert.AreEqual(4, _message2.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void WriteUInt32ShouldWork()
        {
            _message2.WriteInt32(55993874);
            CollectionAssert.AreEqual(new byte[] { 0x12, 0x66, 0x56, 0x03, 0x00, 0x00 }, _message2.Data);
            Assert.AreEqual(4, _message2.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void WriteDataShouldWork()
        {
            _message2.WriteData(new byte[] { 0xCA, 0xC4, 0x70, 0xE1, 0xF2, 0x09  }, 0, 6);
            CollectionAssert.AreEqual(new byte[] { 0xCA, 0xC4, 0x70, 0xE1, 0xF2, 0x09 }, _message2.Data);
            Assert.AreEqual(6, _message2.Length);
        }

        [TestMethod, TestCategory("Unit")]
        public void RewriteInt32ShouldWork()
        {
            _message1.Length = 3;
            _message1.RewriteInt32(0, 1);
            CollectionAssert.AreEqual(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x48, 0xA6 }, _message1.Data);
            Assert.AreEqual(3, _message1.Length);
        }
    }
}