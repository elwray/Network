namespace Jupiter1.Network.Client.Services.ClientConfiguration
{
    public interface IClientConfiguration
    {
        int QPort { get; set; }
        string RemoteHost { get; set; }
        ushort RemotePort { get; set; }
    }
}