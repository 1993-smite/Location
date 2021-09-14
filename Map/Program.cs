using System;
using System.Collections.Generic;
using System.IO;
using DadataLocation;
using System.Linq;

namespace Map
{
    class Program
    {
        static void AddLog(string line)
        {
            using (var sw = new StreamWriter("D:\\log.csv", true))
            {
                sw.WriteLine(line);
            }
        }

        static void Main(string[] args)
        {
            var service = new DaDataLocationService();

            //var list = service.GeoCoding(55.6440658569336, 37.473518371582);
            var lines = new List<OrderLine>();
            using (var sr = new StreamReader("D:\\coord.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var ln = sr.ReadLine().Split(';');

                    var userId = int.Parse(ln[0]);

                    var exist = lines.FirstOrDefault(x => x.UserId == userId);

                    var line = exist
                        ?? new OrderLine
                        {
                            UserId = int.Parse(ln[0]),
                            OrderId = int.Parse(ln[1]),
                            OrderType = ln[2],
                        };
                    line.Location.Lat = ln[4].ToDouble();
                    line.Location.Lon = ln[3].ToDouble();

                    if (exist == null)
                        lines.Add(line);
                }

            }

            foreach(var line in lines)
            {
                var lns = service.GeoCoding(line.Location.Lat, line.Location.Lon);

                var location = lns.FirstOrDefault();

                AddLog($"{line.UserId};{line.OrderId};{line.Location.Lat};{line.Location.Lon};{location.Address};{location.Lat};{location.Lon}");

            }

            Console.WriteLine("Hello World!");
        }
    }
}
