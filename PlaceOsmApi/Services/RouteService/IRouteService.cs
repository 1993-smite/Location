using LocationOsmApi.Models;
using PlaceOsmApi.Models;
using System.Collections.Generic;

namespace PlaceOsmApi.Services.RouteService
{
    public interface IRouteService
    {
        RouteStat[][] Table(IList<Place> places);
        RouteStat Route(IList<Place> places);

        RouteMap[] RouteDetail(IList<Place> places);

        IList<Itinero.Route> RouteDetailItinero(Itinero.Profiles.Vehicle vihicle, IList<Place> places);
    }
}
