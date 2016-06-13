namespace Jupiter1.Network.Server.Services.ServerLocalService
{
    internal interface IServerLocalService
    {
        // <= 1000 / sv_frame->value
        int ResidualTime { get; set; }
        int RestartTime { get; set; }
    }
}