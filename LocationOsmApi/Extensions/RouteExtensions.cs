using Itinero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Extensions
{
    /// <summary>
    /// route extensions
    /// </summary>
    public static class RouteExtensions
    {
        /// <summary>
        /// Concatenate Routes
        /// </summary>
        /// <param name="routes"></param>
        /// <returns></returns>
        public static Route ConcatenateRoutes(this IList<Route> routes)
        {
            var route = new Route();
            for(int index = 0; index < routes.Count(); index++)
            {
                route = route.Concatenate(routes[index]);
            }

            return route;
        }
    }
}
