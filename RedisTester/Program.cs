using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

            ISubscriber sub = redis.GetSubscriber();

            string luigi = "Luigi", zero = "Zero";
            sub.Subscribe(luigi, (channel, message) =>
            {
                Console.WriteLine(luigi + " Send: " + message.ToString());
            });

            sub.Subscribe(zero, (channel, message) =>
            {
                Console.WriteLine(zero + " Send: " + message.ToString());
            });

            sub.Subscribe(luigi).OnMessage(channelMessage => {
                Console.WriteLine(luigi + " Received: " + (string)channelMessage.Message);
            });

            sub.Subscribe(zero).OnMessage(channelMessage => {
                Console.WriteLine(zero + " Received: " + (string)channelMessage.Message);
            });

            int i = 10;
            while (i-- > 0)
            {
                Task.Run(() =>
                {
                    sub.Publish(luigi, "Hello world + " + Thread.CurrentThread.ManagedThreadId);
                    sub.Publish(zero, "Hello world 2 + " + Thread.CurrentThread.ManagedThreadId);
                });
            }

            while (true)
            {

            }
            //IDatabase db = redis.GetDatabase();
            //string key = "myKey";
            //var value = "Luigi";// new List<string> { "Luigi", "Hello", "World", "Wow!" };
            //db.StringSet(key, value);
            ////db.KeyDelete(key);
        }
    }
}
