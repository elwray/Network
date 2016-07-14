using Jupiter1.Network.Core.Extensions;
using Jupiter1.Network.Core.Tests.Collections;
using Jupiter1.Network.Core.Tests.Structure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jupiter1.Network.Core.Tests.Extensions
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        [TestMethod, TestCategory("Unit")]
        public void AssignValueWithValueTypeShouldWork()
        {
            var data = new int[6];
            var actual = data.Assign(21);

            var expected = new[] { 21, 21, 21, 21, 21, 21 };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod, TestCategory("Unit")]
        public void AssignValueWithReferenceTypeShouldWork()
        {
            var data = new ReferenceType[2];
            var actual = data.Assign(new ReferenceType
            {
                Name = "Hello",
                Age = 22
            });

            var expected = new[] { new ReferenceType
            {
                Name = "Hello",
                Age = 22
            }, new ReferenceType
            {
                Name = "Hello",
                Age = 22
            }};

            CollectionAssert.AreEqual(expected, actual, new ReferenceTypeComparer());
        }

        [TestMethod, TestCategory("Unit")]
        public void AssignFuncWithValueTypeShouldWork()
        {
            var data = new int[6];
            var actual = data.Assign(() => 23);

            var expected = new[] { 23, 23, 23, 23, 23, 23 };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod, TestCategory("Unit")]
        public void AssignFuncWithReferenceTypeShouldWork()
        {
            var data = new ReferenceType[2];
            var actual = data.Assign(() => new ReferenceType
            {
                Name = "Hello",
                Age = 22
            });

            var expected = new[] { new ReferenceType
            {
                Name = "Hello",
                Age = 22
            }, new ReferenceType
            {
                Name = "Hello",
                Age = 22
            }};

            CollectionAssert.AreEqual(expected, actual, new ReferenceTypeComparer());
        }
    }
}