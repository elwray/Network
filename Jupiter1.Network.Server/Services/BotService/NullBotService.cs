namespace Jupiter1.Network.Server.Services.BotService
{
    public sealed class NullBotService : IBotService
    {
        #region IBotService
        public void Frame(int ellapsedMilliseconds)
        {
        }
        #endregion
    }
}