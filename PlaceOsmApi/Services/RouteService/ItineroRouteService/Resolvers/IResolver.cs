using Itinero;
using Itinero.LocalGeo;
using Itinero.Profiles;
using System.Collections.Generic;

namespace PlaceOsmApi.Services.RouteService.ItineroRouteService.Resolvers
{
    public interface IResolver
    {
        public Dictionary<float, float> ResolveCoord(Router router, IProfileInstance[] profiles, Coordinate coordinate);
        public Dictionary<float, float> ResolveCoord(Router router, IProfileInstance[] profiles, float lat, float lon);
        public Dictionary<float, float> ResolveCoord(Router router, Itinero.Profiles.Vehicle vihicle, float lat, float lon);
        public void SetResolve(Coordinate coordinate, Coordinate resolveCoordinate);
        public void SetResolve(float lat, float lon, float resolveLat, float resolveLon);
    }
}
