using Itinero.Profiles;
using LocationOsmApi.Models;
using PlaceOsmApi.Models;
using PlaceOsmApi.Services.RouteService;
using PlaceOsmApi.Services.RouteService.ItineroRouteService;
using System;
using System.Collections.Generic;

namespace PlaceOsmApi.Services
{
    public class MapManager: IMapManager
    {
        private LinkedList<IRouteService> routeServices;
        public IRouteService ItineroService { get; private set; }

        public MapManager()
        {
            routeServices = new LinkedList<IRouteService>();
        }

        public MapManager(IList<IRouteService> routeServicesList): this()
        {
            foreach (var routeService in routeServicesList)
                Build(routeService);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeService"></param>
        /// <returns></returns>
        public LinkedList<IRouteService> Build(IRouteService routeService) {
            routeServices.AddLast(routeService);

            if (routeService is ItineroService)
                ItineroService = routeService;

            return routeServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="places"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public RouteStat Route(IList<Place> places, LinkedListNode<IRouteService> service = null)
        {
            RouteStat result = null;
            try
            {
                service = service ?? routeServices.First;
                result = service.Value.Route(places);
            }
            catch (Exception err)
            {
                if (service.Next == null)
                    throw err;
                result = Route(places, service.Next);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="places"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public RouteMap[] RouteDetail(IList<Place> places, LinkedListNode<IRouteService> service = null)
        {
            RouteMap[] result = null;
            try
            {
                service = service ?? routeServices.First;
                result = service.Value.RouteDetail(places);
            }
            catch (Exception err)
            {
                if (service.Next == null)
                    throw err;
                result = RouteDetail(places, service.Next);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="places"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public RouteStat[][] Table(IList<Place> places, LinkedListNode<IRouteService> service = null)
        {
            RouteStat[][] result = null;
            try
            {
                service = service ?? routeServices.First;
                result = service.Value.Table(places);
            }
            catch (Exception err)
            {
                if (service.Next == null)
                    throw err;
                result = Table(places, service.Next);
            }
            return result;
        }

        public IList<Itinero.Route> RouteDetailItinero(Vehicle vihicle, IEnumerable<Place> places)
        {
            return ItineroService.RouteDetailItinero(vihicle, places);
        }
    }
}
