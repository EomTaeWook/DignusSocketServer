using Dignus.Extensions.Log;
using Dignus.Log;

namespace Echo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}DignusLog.config"));
            LogBuilder.Build();

            ServerModule serverModule = new ServerModule();
            serverModule.Run();         
        }
    }
}