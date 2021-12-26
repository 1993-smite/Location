using Itinero;
using Itinero.LocalGeo;
using Itinero.Profiles;
using PlaceOsmApi.Services.Redis;
using System.Collections.Generic;

namespace PlaceOsmApi.Services.RouteService.ItineroRouteService.Resolvers
{
    public class CachedResolver: IResolver
    {
        public const string redisResolverPrefixKey = "resolve";

        private readonly IResolver resolver;
        private readonly RedisClient redisClient;

        public CachedResolver(IResolver resolverItem = null, RedisClient redis = null)
        {
            resolver = resolverItem ?? (IResolver)new Resolver();
            redisClient = redis ?? new RedisClient();
        }

        private string getCoordinateRedisPrefixKey(Coordinate coordinate) =>
            $"{redisResolverPrefixKey}({coordinate.Latitude},{coordinate.Longitude})";

        public Dictionary<float, float> ResolveCoord(Router router, IProfileInstance[] profiles, Coordinate coordinate)
        {
            var result = redisClient.Get<Coordinate?>(getCoordinateRedisPrefixKey(coordinate));
            if (result.HasValue)
            {
                var coord = new Dictionary<float, float>();
                coord.Add(result.Value.Latitude, result.Value.Longitude);
                return coord;
            }
            return resolver.ResolveCoord(router, profiles, coordinate);
        }
        public Dictionary<float, float> ResolveCoord(Router router, IProfileInstance[] profiles, float lat, float lon)
        {
            return ResolveCoord(router, profiles, new Coordinate(lat, lon));
        }

        public Dictionary<float, float> ResolveCoord(Router router, Vehicle vihicle, float lat, float lon)
        {
            var profiles = new IProfileInstance[2] { vihicle.Fastest(), vihicle.Shortest() };

            return ResolveCoord(router, profiles, new Coordinate(lat, lon));
        }

        public void SetResolve(Coordinate coordinate, Coordinate resolveCoordinate)
        {
            redisClient.Set(getCoordinateRedisPrefixKey(coordinate), resolveCoordinate);
        }

        public void SetResolve(float lat, float lon, float resolveLat, float resolveLon)
        {
            redisClient.Set(getCoordinateRedisPrefixKey(new Coordinate(lat, lon)), new Coordinate(resolveLat, resolveLon));
        }
    }
}
