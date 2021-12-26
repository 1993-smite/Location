using StackExchange.Redis;
using System;
using System.Linq;
using System.Net;

namespace Redis
{
    class Program
    {
        static void Main(string[] args)
        {
            IConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = connection.GetDatabase();
            EndPoint endPoint = connection.GetEndPoints().First();
            IServer server = connection.GetServer(endPoint);

            foreach (var key in server.Keys(pattern: "**"))
            {
                Console.WriteLine($"key: {key}, value: {db.StringGet(key)}");
            }

            Console.WriteLine("Hello World!");
        }
    }
}
