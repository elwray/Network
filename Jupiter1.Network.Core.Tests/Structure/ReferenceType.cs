using System.Collections.Generic;

namespace Jupiter1.Network.Core.Tests.Structure
{
    internal class ReferenceType : IEqualityComparer<ReferenceType>
    {
        public string Name { get; set; }
        public int Age { get; set; }

        #region IEqualityComparer
        public bool Equals(ReferenceType x, ReferenceType y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
                return false;

            return x.Name == y.Name && x.Age == y.Age;
        }

        public int GetHashCode(ReferenceType obj)
        {
            return base.GetHashCode();
        }
        #endregion
    }
}