using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Services;
using System;

namespace PlaceOsmApi.Controllers
{
    /// <summary>
    /// GeoPlace
    /// </summary>
    public class GeoPlaceController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected Lazy<IGeoLocationService> lazyGeoLocationService;
        /// <summary>
        /// 
        /// </summary>
        protected IGeoLocationService geoLocationService => lazyGeoLocationService.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geoLocationService"></param>
        public GeoPlaceController(IGeoLocationService geoLocationService)
        {
            lazyGeoLocationService = new Lazy<IGeoLocationService>(()=> geoLocationService);
        }
    }
}
