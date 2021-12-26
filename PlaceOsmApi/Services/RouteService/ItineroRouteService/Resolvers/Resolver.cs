using Itinero;
using Itinero.Data.Network;
using Itinero.Geo;
using Itinero.LocalGeo;
using Itinero.Profiles;
using System.Collections.Generic;

namespace PlaceOsmApi.Services.RouteService.ItineroRouteService.Resolvers
{
    public class Resolver : IResolver
    {
        private double minDistanceInitial = 50;
        private List<Coordinate> coordinates = new List<Coordinate>();
        private Dictionary<float, float> coord = new Dictionary<float, float>();

        public Resolver()
        {

        }

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

        public Dictionary<float, float> ResolveCoord(Router router, IProfileInstance[] profiles, Coordinate coordinate)
        {
            Default();
            var point = new Result<RouterPoint>(new RouterPoint(coordinate.Latitude, coordinate.Longitude, 0, 0));
            var result = new Dictionary<float, float>();
            var minDistance = minDistanceInitial;
            while (result.Count < 4)
            {
                point = router.TryResolve(profiles, coordinate, isBetter, (float)minDistance);
                result = new Dictionary<float, float>(coord);
                minDistance += minDistanceInitial;
            }
            return result;
        }
        public Dictionary<float, float> ResolveCoord(Router router, IProfileInstance[] profiles, float lat, float lon)
        {
            return ResolveCoord(router, profiles, new Coordinate(lat, lon));
        }

        public Dictionary<float, float> ResolveCoord(Router router, Itinero.Profiles.Vehicle vihicle, float lat, float lon)
        {
            var profiles = new IProfileInstance[2] { vihicle.Fastest(), vihicle.Shortest() };

            return ResolveCoord(router, profiles, new Coordinate(lat, lon));
        }

        public void SetResolve(Coordinate coordinate, Coordinate resolveCoordinate)
        {
            //throw new System.NotImplementedException();
        }

        public void SetResolve(float lat, float lon, float resolveLat, float resolveLon)
        {
            //throw new System.NotImplementedException();
        }
    }
}
