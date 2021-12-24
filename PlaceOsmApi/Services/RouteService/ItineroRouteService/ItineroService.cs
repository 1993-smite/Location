using Itinero;
using Itinero.Data.Network;
using Itinero.IO.Osm;
using Itinero.LocalGeo;
using Itinero.Profiles;
using LocationOsmApi.Models;
using PlaceOsmApi.Models;
using PlaceOsmApi.Services.Route.ItineroRouteService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;

namespace PlaceOsmApi.Services.RouteService.ItineroRouteService
{
    /// <summary>
    /// itinero service
    /// </summary>
    public class ItineroService: CacheService<Router>, IRouteService
    {
        private readonly string filePath;
        private Lazy<Router> lazyRouter;
        private Router Router => lazyRouter.Value;
        private Vehicle RouterProfile;

        private Resolver resolver;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="file"></param>
        /// <param name="memoryCache"></param>
        /// <param name="periodCache"></param>
        public ItineroService(string file, MemoryCache memoryCache = null, TimeSpan? periodCache = null): base(memoryCache, periodCache)
        {
            filePath = file;
            RouterProfile = Itinero.Osm.Vehicles.Vehicle.Car;
            lazyRouter = new Lazy<Router>(()=> ItineroRouter.GetRouter(RouterProfile));
            resolver = new Resolver();
        }

        /// <summary>
        /// get cached router
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public Router GetCachedRouter(Vehicle profile)
        {
            Router router;
            router = GetItem(() =>
            {
                var routerDb = new RouterDb();
                using (var stream = new FileInfo(@"D:\Moscow.osm.pbf").OpenRead())
                {
                    routerDb.LoadOsmData(stream, Itinero.Osm.Vehicles.Vehicle.Car, Itinero.Osm.Vehicles.Vehicle.Pedestrian);
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

        public IList<Itinero.Route> RouteDetailItinero(Vehicle vehicle, IList<Place> places)
        {
            var result = new List<Itinero.Route>();

            for (int index = 1; index < places.Count; index++)
                result.Add(RouteDetailItinero(vehicle, places[index - 1], places[index]));

            return result;
        }

        public IList<Itinero.Route> RouteDetailItineroAsParallel(Vehicle vehicle, IList<Place> places)
        {
            var result = new List<Itinero.Route>();

            places
                .Select((x, index) => new { Index = index, Item = x })
                .AsParallel()
                .Skip(1)
                .Select(x=> RouteDetailItinero(vehicle, places[x.Index-1], places[x.Index]));

            return result;
        }

        private Itinero.Route RouteDetailItinero(Vehicle vehicle, Place from, Place to)
        {
            var result = new Itinero.Route();

            var starts = resolver.ResolveCoord(Router, vehicle, (float)from.Latitute, (float)from.Longitude);

            var ends = resolver.ResolveCoord(Router, vehicle, (float)to.Latitute, (float)to.Longitude);

            bool isBuilded = false;
            foreach (var st in starts)
            {
                foreach (var en in ends)
                {
                    var res = Router.TryCalculate(vehicle.Fastest(), st.Key, st.Value, en.Key, en.Value);
                    if (!res.IsError && res.Value != null)
                    {
                        result = res.Value;
                        isBuilded = true;
                        break;
                    }
                }
                if (isBuilded)
                    break;
            }

            return result;
        }
    }
}
