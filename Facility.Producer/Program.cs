using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

namespace Facility.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Fatal);

            var configuration = new BusConfiguration();
            configuration.EndpointName("Chocolate.Facility.Producer");

            configuration.UseTransport<MsmqTransport>();
            configuration.UsePersistence<InMemoryPersistence>();

            var bus = Bus.Create(configuration).Start();
            stopWatch.Stop();

            Console.WriteLine($"Initalizing the bus took { stopWatch.Elapsed.ToString("G")}");
            stopWatch.Reset();
            stopWatch.Start();
            Syncher.SyncEvent.Wait();
            stopWatch.Stop();
            Console.WriteLine($"Receiving #{ Syncher.SyncEvent.InitialCount } of msgs over the bus took { stopWatch.Elapsed.ToString("G")}");

            Console.ReadLine();
        }
    }
}
