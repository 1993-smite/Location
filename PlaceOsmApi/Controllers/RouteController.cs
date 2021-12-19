﻿using Itinero;
using Itinero.Osm.Vehicles;
using LocationOsmApi.Models;
using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Extensions;
using PlaceOsmApi.Services;
using System.Collections.Generic;
using System.IO;

namespace PlaceOsmApi.Controllers
{
    /// <summary>
    /// route controller
    /// </summary>
    [Route("route/[action]")]
    [ApiController]
    public class RouteController : MapController
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mapManager"></param>
        /// <param name="geoLocationService"></param>
        public RouteController(IMapManager mapManager, IGeoLocationService geoLocationService)
            : base(mapManager, geoLocationService)
        {

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

            for(int index = 0;index<places.Count;index++)
            {
                var place = places[index];
                if (place.Lat == null && place.Lon == null && !string.IsNullOrEmpty(place.Address))
                {
                    places[index] = geoLocationService.GetPlaceByAddress(place.Address);
                }
            }

            var routes = MapManager.RouteDetailItinero(profile, places);
            var route = routes.ConcatenateRoutes();
            using (var writer = new StreamWriter(@"D:\logs\route.geojson"))
            {
                route.WriteGeoJson(writer);
            }

            return Ok(route.ToGeoJson());
        }

    }
}
