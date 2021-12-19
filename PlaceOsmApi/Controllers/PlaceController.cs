using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Services;
using System;

namespace LocationOsmApi.Controllers
{
    /// <summary>
    /// place controller
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        Lazy<IGeoLocationService> lazyGeoLocationService;
        IGeoLocationService GeoLocationService => lazyGeoLocationService.Value;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="geoService"></param>
        public PlaceController(IGeoLocationService geoService)
        {
            lazyGeoLocationService = new Lazy<IGeoLocationService>(()=> geoService);
        }

        /// <summary>
        /// get place by address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPlace(string address)
        {
            var res = GeoLocationService.GetPlaceByAddress(address);
            return Ok(res);
        }

        /// <summary>
        /// get place by geo data
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPlaceByGeo(double lat, double lon)
        {
            var res = GeoLocationService.GetPlaceByGeo(lat, lon);
            return Ok(res);
        }

    }
}
