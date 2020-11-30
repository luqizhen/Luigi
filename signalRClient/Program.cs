using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace signalRClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:8080/signalr/hubs");
            connection.Reconnected += () => Console.WriteLine("Reconnected.");
            connection.Closed += async () =>
            {
                Console.WriteLine("Closed");
                Thread.Sleep(5 * 1000);
                await connection.Start();
            };

            Task.Run(connection.Start);


            while (true)
            {
                ;
            }
        }
    }
}
