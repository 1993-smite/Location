using LocationOsmApi.Models;
using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Services;
using System.Collections.Generic;

namespace PlaceOsmApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RouteController : MapController
    {

        public RouteController(IMapManager mapManager, IGeoLocationService geoLocationService)
            : base(mapManager, geoLocationService)
        {

        }

        [HttpPost]
        public IActionResult GetRoute(IList<Place> places)
        {
            return Ok(MapManager.Route(places));
        }

        [HttpPost]
        public IActionResult GetRouteDetails(IList<Place> places)
        {
            return Ok(MapManager.RouteDetail(places));
        }

    }
}
