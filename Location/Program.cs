using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var routerDb = new RouterDb();
            using (var stream = new FileInfo(@"D:\antarctica-latest.osm.pbf").OpenRead())
            {
                // create the network for cars only.
                routerDb.LoadOsmData(stream, Vehicle.Car);
            }

            var router = new Router(routerDb);

            // get a profile.
            var profile = Vehicle.Car.Fastest(); // the default OSM car profile.

            // create a routerpoint from a location.
            // snaps the given location to the nearest routable edge.
            var start = router.Resolve(profile, 51.26797020271655f, 4.801905155181885f);
            var end = router.Resolve(profile, 51.26797020271655f, 4.801905155181885f);

            // calculate a route.
            var route = router.Calculate(profile, start, end);


            Console.WriteLine("Hello World!");
        }
    }
}
