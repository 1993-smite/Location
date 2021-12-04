using Itinero;
using Itinero.Geo;
using Itinero.Osm.Vehicles;
using Itinero.IO.Osm;
using System;
using System.IO;
using System.Collections.Generic;

namespace OsmSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var routerDb = new RouterDb();
            using (var stream = new FileInfo(@"D:\Moscow.osm.pbf").OpenRead())
            {
                routerDb.LoadOsmData(stream, Vehicle.Car);
            }
            var router = new Router(routerDb);


            // calculate a route.
            var route = router.Calculate(Vehicle.Car.Fastest(),
                    55.875592017566014f, 37.512952535605685f
                    , 55.87671837280051f, 37.514387730178676f
                );
                //.Calculate(Vehicle.Car.Fastest(),
                //55.75886171421706f, 37.61620156137363f, 55.76018380124042f, 37.61859256982013f);
            var geoJson = route.ToGeoJson();

            Console.WriteLine($"->  Distance: {route.TotalDistance}");
            Console.WriteLine($"->  Time: {route.TotalTime}");
        }
    }
}
