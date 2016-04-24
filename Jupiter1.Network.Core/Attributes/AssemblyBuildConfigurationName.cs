using System;

namespace Jupiter1.Network.Core.Attributes
{
    public sealed class AssemblyBuildConfigurationName : Attribute
    {
        public string ConfigurationName { get; private set; }

        public AssemblyBuildConfigurationName(string configurationName)
        {
            if (configurationName == null)
                throw new ArgumentNullException(nameof(configurationName));

            ConfigurationName = configurationName;
        }
    }
}