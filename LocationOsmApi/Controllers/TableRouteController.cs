using LocationOsmApi.Models;
using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Services;
using System.Collections.Generic;

namespace PlaceOsmApi.Controllers
{
    /// <summary>
    /// Table Route Controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TableRouteController : MapController
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mapManager"></param>
        /// <param name="geoLocationService"></param>
        public TableRouteController(IMapManager mapManager, IGeoLocationService geoLocationService): base(mapManager, geoLocationService)
        {

        }

        /// <summary>
        /// table route
        /// </summary>
        /// <param name="places"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult TableRoute(IList<Place> places)
        {
            return Ok(MapManager.Table(places));
        }
    }
}
