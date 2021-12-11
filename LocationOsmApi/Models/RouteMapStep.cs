using LocationOsmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Models
{
    public class RouteMapStep: Place
    {
        public readonly RouteStat RouteStat;

        public RouteMapStep(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }

        public RouteMapStep(double lat, double lon, double dist, double time): this(lat, lon)
        {
            RouteStat = new RouteStat(dist, time);
        }

        public RouteMapStep(double lat, double lon, double dist): this(lat, lon, dist, 0)
        {
        }
    }
}
