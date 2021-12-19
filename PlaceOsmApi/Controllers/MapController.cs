using PlaceOsmApi.Services;
using System;

namespace PlaceOsmApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class MapController : GeoPlaceController
    {
        /// <summary>
        /// 
        /// </summary>
        protected Lazy<IMapManager> lazyMapManager;

        /// <summary>
        /// 
        /// </summary>
        protected IMapManager MapManager => lazyMapManager.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapManager"></param>
        /// <param name="geoLocationService"></param>
        public MapController(IMapManager mapManager, IGeoLocationService geoLocationService): base(geoLocationService)
        {
            lazyMapManager = new Lazy<IMapManager>(() => mapManager);
        }
    }
}
