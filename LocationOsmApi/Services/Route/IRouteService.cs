﻿using LocationOsmApi.Models;
using PlaceOsmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Services
{
    public interface IRouteService
    {
        RouteStat[][] Table(IList<Place> places);
        RouteStat Route(IList<Place> places);

        RouteMap[] RouteDetail(IList<Place> places);
    }
}