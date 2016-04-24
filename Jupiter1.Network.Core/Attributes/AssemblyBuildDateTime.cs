using System;
using System.Globalization;

namespace Jupiter1.Network.Core.Attributes
{
    public sealed class AssemblyBuildDateTime : Attribute
    {
        public DateTime BuildDateTime { get; set; }

        public AssemblyBuildDateTime(string dateTime)
        {
            if (dateTime == null)
                throw new ArgumentNullException(nameof(dateTime));

            DateTime result;
            if (DateTime.TryParseExact(dateTime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out result))
            {
                BuildDateTime = result;
            }
        }
    }
}