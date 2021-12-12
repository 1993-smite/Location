using LocationOsmApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Models;
using PlaceOsmApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RouteController : MapController
    {

        public RouteController(IMapManager mapManager): base(mapManager)
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
