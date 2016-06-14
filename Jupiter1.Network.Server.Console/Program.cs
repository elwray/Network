using Jupiter1.Network.Server.Services.ServerConfiguration;

namespace Jupiter1.Network.Server.Console
{
    class Program
    {
        public static void Main(string[] args)
        {
            var server = ServerFactory.GetService(new ServerConfiguration());
            for (var i = 0; i < 1000; ++i)
                server.Frame(i);
        }
    }
}