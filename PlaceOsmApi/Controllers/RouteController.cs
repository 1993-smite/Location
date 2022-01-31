using Itinero;
using Itinero.Osm.Vehicles;
using LocationOsmApi.Models;
using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Extensions;
using PlaceOsmApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using PlaceOsmApi.Services.ClusterizationService;
using System.Linq;

namespace PlaceOsmApi.Controllers
{
    /// <summary>
    /// route controller
    /// </summary>
    [Route("route/[action]")]
    [ApiController]
    public class RouteController : MapController
    {
        private readonly ILogger<RouteController> _logger;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mapManager"></param>
        /// <param name="geoLocationService"></param>
        public RouteController(IMapManager mapManager, IGeoLocationService geoLocationService, ILogger<RouteController> logger)
            : base(mapManager, geoLocationService)
        {
            _logger = logger;

            _logger.LogDebug("Route request");
        }

        /// <summary>
        /// get route
        /// </summary>
        /// <param name="places"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetRoute(IList<Place> places)
        {
            return Ok(MapManager.Route(places));
        }

        /// <summary>
        /// get route details
        /// </summary>
        /// <param name="places"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetRouteDetails(IList<Place> places)
        {
            return Ok(MapManager.RouteDetail(places));
        }

        /// <summary>
        /// get route details itinero
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="places"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/route/details/{vehicle}")]
        public IActionResult GetRouteDetailsItinero(string vehicle, IList<Place> places)
        {
            Itinero.Profiles.Vehicle profile = Vehicle.Pedestrian;
            switch (vehicle?.ToLower())
            {
                case "car":
                    profile = Vehicle.Car;
                    break;
                case "step":
                    profile = Vehicle.Pedestrian;
                    break;
                case "bus":
                    profile = Vehicle.Bus;
                    break;
            }

            Route route = new Route();

            try
            {
                for (int index = 0; index < places.Count; index++)
                {
                    var place = places[index];
                    if (place.Lat == null && place.Lon == null && !string.IsNullOrEmpty(place.Address))
                    {
                        places[index] = geoLocationService.GetPlaceByAddress(place.Address);
                    }
                }

                var routes = MapManager.RouteDetailItinero(profile, places);
                route = routes.ConcatenateRoutes();

                using (var writer = new StreamWriter(@"D:\logs\route.geojson"))
                {
                    route.WriteGeoJson(writer, false, true, false, null);
                }
            }
            catch (Exception err)
            {
                _logger.LogWarning(err.StackTrace);
                _logger.LogWarning(err.Message);
            }

            return Ok(route.ToGeoJson(false, true, false));
        }

        /// <summary>
        /// get route details itinero
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="places"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/route/details/car/{carCount}")]
        public IActionResult GetRoutes(int carCount, IList<Place> places)
        {
            Itinero.Profiles.Vehicle profile = Vehicle.Car;

            var clusterRoutes = new List<Route>(carCount);

            try
            {
                for (int index = 0; index < places.Count; index++)
                {
                    var place = places[index];
                    if (place.Lat == null && place.Lon == null && !string.IsNullOrEmpty(place.Address))
                    {
                        places[index] = geoLocationService.GetPlaceByAddress(place.Address);
                    }
                }

                IClusterize clusterize = new KmeansClusterize();
                var route = new Route();
                var placeClusters = clusterize.Clusterize(carCount, places);

                foreach(var cluster in placeClusters)
                {
                    var routes = MapManager.RouteDetailItinero(profile, cluster);
                    route = routes.ConcatenateRoutes();
                    clusterRoutes.Add(route);
                }

                using (var writer = new StreamWriter(@"D:\logs\route.geojson"))
                {
                    route.WriteGeoJson(writer, false, true, false, null);
                }
            }
            catch (Exception err)
            {
                _logger.LogWarning(err.StackTrace);
                _logger.LogWarning(err.Message);
            }

            return Ok(clusterRoutes.AsParallel().Select(x => x.ToGeoJson(false, true, false)));
        }

    }
}
