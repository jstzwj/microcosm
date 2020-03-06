using Microcosm.Server;
using System;
using System.Net;

namespace Microcosm.Demo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Microcosm!");

            var settings = new ServerSettings();
            settings.LocalAddress = IPAddress.Any;
            settings.LocalPort = 25588;
            settings.EnableCluster = false;

            Microcosm.Server.Server server = new Microcosm.Server.Server(settings);
            server.Startup();
        }
    }
}
