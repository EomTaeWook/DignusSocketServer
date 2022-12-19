using Kosher.Extensions.Log;
using Kosher.Log;

namespace Echo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}KosherLog.config"));
            LogBuilder.Build();

            ServerModule serverModule = new ServerModule();

            serverModule.Run();
        }
    }
}