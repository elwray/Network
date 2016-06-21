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
            var array = new ReferenceTypeClass[2];
            array.Assign(() => new ReferenceTypeClass
            {
                FirstName = "Hello",
                LastName = "World",
                Age = 22
            });
            CollectionAssert.AreEqual(new[] { new ReferenceTypeClass
            {
                FirstName = "Hello",
                LastName = "World",
                Age = 22
            }, new ReferenceTypeClass
            {
                FirstName = "Hello",
                LastName = "World",
                Age = 22
            }}, array);
        }

        [TestMethod, TestCategory("Unit")]
        public void AssignWithReferenceTypeShouldWork()
        {
            var array = new int[6];
            array.Assign(() => 20);
            CollectionAssert.AreEqual(new[] { 20, 20, 20, 20, 20, 20 }, array);
        }
    }
}