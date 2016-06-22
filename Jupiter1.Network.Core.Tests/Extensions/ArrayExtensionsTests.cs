using Jupiter1.Network.Core.Extensions;
using Jupiter1.Network.Core.Tests.Structure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jupiter1.Network.Core.Tests.Extensions
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        [TestMethod, TestCategory("Unit")]
        public void AssignWithValueTypeShouldWork()
        {
            var actual = new int[6];
            actual.Assign(() => 20);
            var expected = new[] { 20, 20, 20, 20, 20, 20 };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod, TestCategory("Unit")]
        public void AssignWithReferenceTypeShouldWork()
        {
            var actual = new ReferenceTypeClass[2];
            actual.Assign(() => new ReferenceTypeClass
            {
                Name = "Hello",
                Age = 22
            });

            var expected = new[] { new ReferenceTypeClass
            {
                Name = "Hello",
                Age = 22
            }, new ReferenceTypeClass
            {
                Name = "Hello",
                Age = 22
            }};

            CollectionsAreEqual(expected, actual);
        }

        private void CollectionsAreEqual(ReferenceTypeClass[] expected, ReferenceTypeClass[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i].Name, actual[i].Name);
                Assert.AreEqual(expected[i].Age, actual[i].Age);
            }
        }
    }
}