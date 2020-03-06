using Microcosm.Gateway;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Microcosm.Demo.Gateway
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Microcosm!");
            Task.Run(
                async () => await Startup()
                ).Wait();
        }

        private static async Task Startup()
        {
            var settings = new GatewaySettings();
            settings.LocalAddress = IPAddress.Any;
            settings.LocalPort = 26677;
            settings.EnableCluster = false;

            Microcosm.Gateway.Gateway gateway = new Microcosm.Gateway.Gateway(settings);
            await gateway.Connect();
            gateway.CreateActor();
        }
    }
}
