using LocationOsmApi.Models;
using Osrm.Client;
using Osrm.Client.Models;
using Osrm.Client.v5;
using PlaceOsmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class OsrmService: IRouteService
    {
        public string OsrmApiLink { get; private set; }
        private Lazy<Osrm5x> lazyOsrm5X;
        private Osrm5x osrm5X => lazyOsrm5X.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="osrmApiLink"></param>
        public OsrmService(string osrmApiLink)
        {
            OsrmApiLink = osrmApiLink;
            lazyOsrm5X = new Lazy<Osrm5x>(()=> new Osrm5x(OsrmApiLink));
        }

        private Location[] toLocations(IList<Place> places)
        {
            var locations = new Location[places.Count];
            for (int index = 0; index < places.Count; index++)
                locations[index] = new Location(places[index].Latitute, places[index].Longitude);

            return locations;
        }

        /// <summary>
        /// get table RouteStat[][]
        /// </summary>
        /// <param name="places"></param>
        /// <returns></returns>
        public RouteStat[][] Table(IList<Place> places)
        {
            var locations = toLocations(places);

            var responce = osrm5X.Table(locations);
            var stats = new RouteStat[places.Count][];
            for (int y = 0; y < places.Count; y++)
            {
                stats[y] = new RouteStat[places.Count];
                for (int x = 0; x < places.Count; x++)
                {
                    stats[y][x] = new RouteStat(responce.Destinations[y].Distance, responce.Durations[y][x]);
                }
            }

            return stats;
        }

        /// <summary>
        /// get array RouteMap[]
        /// </summary>
        /// <param name="places"></param>
        /// <returns></returns>
        public RouteStat Route(IList<Place> places)
        {
            var locations = toLocations(places);

            var response = osrm5X.Route(new RouteRequest() {
               Coordinates = locations,
               Steps = false
            });

            var rt = response.Routes.FirstOrDefault();

            var route = new RouteStat(rt.Distance, rt.Duration);
            return route;
        }

        public RouteMap[] RouteDetail(IList<Place> places)
        {
            var locations = toLocations(places);

            var response = osrm5X.Route(new RouteRequest()
            {
                Coordinates = locations,
                Steps = false
            });

            var route = new RouteMap[places.Count];
            int index = -1;
            foreach (var rt in response.Routes)
            {
                route[++index] = new RouteMap();
                foreach (var leg in rt.Legs)
                {
                    foreach (var step in leg.Steps)
                    {
                        var geom = step.Geometry.FirstOrDefault();
                        route[index].Add(new RouteMapStep(geom.Latitude, geom.Longitude, step.Distance, step.Duration));
                    }
                }
            }

            return route;
        }
    }
}
