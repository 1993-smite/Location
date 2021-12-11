using LocationOsmApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Controllers;
using PlaceOsmApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationOsmApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PlaceController : MapController
    {
        Lazy<IGeoLocationService> lazyGeoLocationService;
        IGeoLocationService GeoLocationService => lazyGeoLocationService.Value;

        public PlaceController(IGeoLocationService geoService): base()
        {
            lazyGeoLocationService = new Lazy<IGeoLocationService>(()=> geoService);
        }

        [HttpGet]
        public IActionResult GetPlace(string address)
        {
            var res = GeoLocationService.GetPlaceByAddress(address);
            return Ok(res);
        }

        //[HttpGet]
        //public async Task<Place> GetPlaceAsync(string address)
        //{
        //    var res = await (GeoLocationService as DadataService).GetPlaceByAddressAsync(address);
        //    return res;
        //}

        [HttpGet]
        public IActionResult GetPlaceByGeo(double lat, double lon)
        {
            var res = GeoLocationService.GetPlaceByGeo(lat, lon);
            return Ok(res);
        }

    }
}
