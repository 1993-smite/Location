using DBDapper.Models;
using DBDapper.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TaskWorker
{
    class Program
    {
        static class Keys
        {
            public static IConfigurationRoot Configuration;
            private static string ConnectionStringKey => "connectionString";
            private static string FileKey => "file";
            private static string GetConfigValue(string key) => Configuration.GetSection(key).Value;
            public static string ConnectionString => GetConfigValue(ConnectionStringKey);
            public static string File => GetConfigValue(FileKey);
        }

        static void Init()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app.json");

            Keys.Configuration = builder.Build();
        }

        static void Main(string[] args)
        {
            Init();

            //Runner.Run();
            var lu = new LoadUsers(Keys.ConnectionString);
            lu.Run(Keys.File);


            Console.WriteLine("The End!");
        }
    }
}
