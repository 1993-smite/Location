using Itinero;
using Itinero.Osm.Vehicles;
using LocationOsmApi.Models;
using Microsoft.AspNetCore.Mvc;
using PlaceOsmApi.Extensions;
using PlaceOsmApi.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        [HttpPost]
        [Route("/{vehicle}")]
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
