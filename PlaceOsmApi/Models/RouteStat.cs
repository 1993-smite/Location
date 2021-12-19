using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Models
{
    public class RouteStat
    {
        public double Distance { get; private set; }
        public double Duration { get; private set; }

        public RouteStat(double distance, double duration)
        {
            Distance = distance;
            Duration = duration;
        }
    }
}
