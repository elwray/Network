namespace Jupiter1.Network.Client.Services.ClientConfiguration
{
    public sealed class ClientConfiguration : IClientConfiguration
    {
        #region IClientConfiguration
        public int QPort { get; set; }
        public string RemoteHost { get; set; }
        public ushort RemotePort { get; set; }
        #endregion
    }
}