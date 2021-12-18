using Itinero;
using Itinero.Data.Network;
using Itinero.IO.Osm;
using Itinero.LocalGeo;
using Itinero.Osm.Vehicles;
using Itinero.Profiles;
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
        private Itinero.Profiles.Vehicle RouterProfile;

        public ItineroService(string file, MemoryCache memoryCache = null, TimeSpan? periodCache = null): base(memoryCache, periodCache)
        {
            filePath = file;
            RouterProfile = Itinero.Osm.Vehicles.Vehicle.Car;
            lazyRouter = new Lazy<Router>(()=> GetRouter(RouterProfile));
        }

        public Router GetRouter(Itinero.Profiles.Vehicle profile)
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

        private List<Coordinate> coordinates = new List<Coordinate>();
        private Dictionary<float, float> coord = new Dictionary<float, float>();
        private bool isBetter(RoutingEdge edge)
        {
            foreach (var shape in edge.Shape)
            {
                if (!coord.ContainsKey(shape.Latitude))
                {
                    coord.Add(shape.Latitude, shape.Longitude);
                }
            }

            return false;
        }

        private void Default()
        {
            coordinates.Clear();
            coord.Clear();
        }

        private Dictionary<float, float> ResolveCoord(IProfileInstance[] profiles, Coordinate coordinate) 
        {
            Default();
            var point = new Result<RouterPoint>(new RouterPoint(coordinate.Latitude, coordinate.Longitude, 0, 0));
            var result = new Dictionary<float, float>();
            var minDistance = 50;
            while (result.Count < 4)
            {
                point = Router.TryResolve(profiles, coordinate, isBetter, minDistance);
                result = new Dictionary<float, float>(coord);
                minDistance += 50;
            }
            return result;
        }
        private Dictionary<float, float> ResolveCoord(IProfileInstance[] profiles, float lat, float lon)
        {
            return ResolveCoord(profiles, new Coordinate(lat, lon));
        }


        public IList<Route> RouteDetailItinero(Itinero.Profiles.Vehicle vihicle, IList<Place> places)
        {
            RouterProfile = vihicle;
            //Router.VerifyAllStoppable = true;

            var profiles = new IProfileInstance[2] { vihicle.Fastest(), vihicle.Shortest() };

            var result = new List<Route>();

            bool isBuilded;

            for (int index = 1; index < places.Count; index++)
            {
                isBuilded = false;
                var from = places[index - 1];
                var to = places[index];

                var starts = ResolveCoord(profiles, (float)from.Latitute, (float)from.Longitude);

                var ends = ResolveCoord(profiles, (float)to.Latitute, (float)to.Longitude);

                
                foreach (var st in starts)
                {
                    foreach (var en in ends)
                    {
                        var res = Router.TryCalculate(vihicle.Fastest(), st.Key, st.Value, en.Key, en.Value);
                        if (!res.IsError && res.Value != null)
                        {
                            result.Add(res.Value);
                            isBuilded = true;
                            break;
                        }
                    }
                    if (isBuilded)
                        break;
                }
            }

            return result;
        }
    }
}
