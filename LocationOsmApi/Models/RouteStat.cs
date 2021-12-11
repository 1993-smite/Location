using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Models
{
    public class RouteStat
    {
        public readonly double Distance;
        public readonly double Duration;

        public RouteStat(double distance, double duration)
        {
            Distance = distance;
            Duration = duration;
        }
    }
}
