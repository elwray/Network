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
            var actual = new ReferenceType[2];
            actual.Assign(() => new ReferenceType
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