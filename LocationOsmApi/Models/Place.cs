using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationOsmApi.Models
{
    public class Place
    {
        public string Address { get; set; }
        public double? Lat { get; set; }
        public double Latitute => Lat ?? 0;
        public double? Lon { get; set; }
        public double Longitude => Lon ?? 0;

        public Place()
        {

        }

        public Place(string address): this()
        {
            Address = address;
        }

        public Place(double lat, double lon) : this()
        {
            Lat = lat;
            Lon = lon;
        }

        public Place(string address, double lat, double lon): this(lat, lon)
        {
            Address = address;
        }
    }
}
