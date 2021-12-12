using LocationOsmApi.Models;
using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Services;
using System.Collections.Generic;

namespace PlaceOsmApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TableRouteController : MapController
    {
        public TableRouteController(IMapManager mapManager, IGeoLocationService geoLocationService): base(mapManager, geoLocationService)
        {

        }

        [HttpGet]
        public IActionResult TableRoute(IList<Place> places)
        {
            return Ok(MapManager.Table(places));
        }
    }
}
