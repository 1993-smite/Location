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
    public class TableRouteController : MapController
    {
        public TableRouteController(IMapManager mapManager): base(mapManager)
        {

        }

        [HttpGet]
        public IActionResult TableRoute(IList<Place> places)
        {
            return Ok(MapManager.Table(places));
        }
    }
}
