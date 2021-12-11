using LocationOsmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Services
{
    public interface IGeoLocationService
    {
        Place GetPlaceByAddress(string address);
        Place GetPlaceByGeo(double lat, double lon);
    }
}
