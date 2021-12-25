using Itinero;
using Itinero.IO.Osm;
using System.IO;
using System.Threading.Tasks;

namespace PlaceOsmApi.Services.RouteService.ItineroRouteService
{
    /// <summary>
    /// static itinero router
    /// </summary>
    public static class ItineroRouter
    {
        public static Router RtRouter;

        static ItineroRouter()
        {
            var routerDb = new RouterDb();
            using (var stream = new FileInfo(@"D:\Moscow.osm.pbf").OpenRead())
            {
                routerDb.LoadOsmData(stream, Itinero.Osm.Vehicles.Vehicle.Car, Itinero.Osm.Vehicles.Vehicle.Pedestrian);
            }
            RtRouter = new Router(routerDb);
        }
    }
}
