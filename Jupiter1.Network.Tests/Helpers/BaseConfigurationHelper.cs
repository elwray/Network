namespace Jupiter1.Network.Tests.Helpers
{
    public abstract class BaseConfigurationHelper<T> where T : class
    {
        public abstract T CreateConfiguration();
    }
}