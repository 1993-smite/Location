using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Services;
using System;

namespace LocationOsmApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        Lazy<IGeoLocationService> lazyGeoLocationService;
        IGeoLocationService GeoLocationService => lazyGeoLocationService.Value;

        public PlaceController(IGeoLocationService geoService)
        {
            lazyGeoLocationService = new Lazy<IGeoLocationService>(()=> geoService);
        }

        [HttpGet]
        public IActionResult GetPlace(string address)
        {
            var res = GeoLocationService.GetPlaceByAddress(address);
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetPlaceByGeo(double lat, double lon)
        {
            var res = GeoLocationService.GetPlaceByGeo(lat, lon);
            return Ok(res);
        }

    }
}
