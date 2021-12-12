using LocationOsmApi.Models;
using PlaceOsmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Services
{
    public interface IMapManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeService"></param>
        /// <returns></returns>
        public LinkedList<IRouteService> Build(IRouteService routeService);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="places"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public RouteStat Route(IList<Place> places, LinkedListNode<IRouteService> service = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="places"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public RouteMap[] RouteDetail(IList<Place> places, LinkedListNode<IRouteService> service = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="places"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public RouteStat[][] Table(IList<Place> places, LinkedListNode<IRouteService> service = null);
    }
}
