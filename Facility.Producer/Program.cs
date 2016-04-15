using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
            ServicePointManager.DefaultConnectionLimit = 5000; // default settings only allows 2 concurrent requests per process to the same host
            ServicePointManager.UseNagleAlgorithm = false; // optimize for small requests
            ServicePointManager.Expect100Continue = false; // reduces number of http calls
            ServicePointManager.CheckCertificateRevocationList = false; // optional, only disable if all dependencies are trusted 

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();
            defaultFactory.Level(LogLevel.Fatal);

            var configuration = new BusConfiguration();
            configuration.EndpointName("Chocolate.Facility.Producer");

            configuration.UseTransport<AzureStorageQueueTransport>().BatchSize(32)
                .ConnectionString("FILLIN");
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.UseSerialization<JsonSerializer>();

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
