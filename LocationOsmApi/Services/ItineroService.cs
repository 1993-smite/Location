using Itinero;
using Itinero.IO.Osm;
using Itinero.Osm.Vehicles;
using LocationOsmApi.Models;
using PlaceOsmApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace PlaceOsmApi.Services
{
    public class ItineroService: CacheService<Router>, IRouteService
    {
        private readonly string filePath;
        private Lazy<Router> lazyRouter;
        private Router Router => lazyRouter.Value;
        private readonly Itinero.Profiles.Vehicle RouterProfile;

        public ItineroService(string file, MemoryCache memoryCache = null, TimeSpan? periodCache = null): base(memoryCache, periodCache)
        {
            filePath = file;
            lazyRouter = new Lazy<Router>(()=> GetRouter());
            RouterProfile = Vehicle.Car;
        }

        public Router GetRouter()
        {
            Router router;
            router = GetItem(() =>
            {
                var routerDb = new RouterDb();
                using (var stream = new FileInfo(filePath).OpenRead())
                {
                    routerDb.LoadOsmData(stream, RouterProfile);
                }
                router = new Router(routerDb);
                return router;
            });

            return router;
        }

        public RouterPoint[] ToRouterPoint(IList<Place> places)
        {
            var points = new RouterPoint[places.Count];
            for (int index = 0; index < places.Count; index++)
                points[index] = new RouterPoint((float)places[index].Latitute, (float)places[index].Longitude, 0, 0);
            return points;
        }

        public RouteStat[][] Table(IList<Place> places)
        {
            var pnts = ToRouterPoint(places);
            var tbls = Router.CalculateWeight(RouterProfile.Fastest(), pnts, new HashSet<int>(0));
            var stats = new RouteStat[tbls.Length][];
            for (int y = 0; y < tbls.Length; y++)
            {
                stats[y] = new RouteStat[tbls[y].Length];
                for (int x = 0; x < tbls[y].Length; x++)
                {
                    stats[y][x] = new RouteStat(tbls[y][x], 0);
                }
            }
            return stats;
        }

        public RouteStat Route(IList<Place> places)
        {
            var pnts = ToRouterPoint(places);
            var route = Router.Calculate(RouterProfile.Fastest(), pnts);

            var result = new RouteStat(route.TotalDistance, 0);
            return result;
        }

        public RouteMap[] RouteDetail(IList<Place> places)
        {
            var pnts = ToRouterPoint(places);
            var route = Router.Calculate(RouterProfile.Fastest(), pnts);

            var rtMap = new RouteMap[places.Count];
            foreach (var pos in route)
            {
                rtMap[0].Add(new RouteMapStep(pos.Location().Latitude, pos.Location().Longitude));
            }


            return rtMap;
        }
    }
}
