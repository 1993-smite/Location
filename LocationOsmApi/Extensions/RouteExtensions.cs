using Itinero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Extensions
{
    public static class RouteExtensions
    {
        public static Route ConcatenateRoutes(this IList<Route> routes)
        {
            var route = new Route();
            for(int index = 0; index < routes.Count(); index++)
            {
                route = route.ConcatenateRoute(routes[index]);
            }

            return route;
        }

        public static Route ConcatenateRoute(this Route route, Route route1)
        {
            //route.Shape.Append(route1.Shape);
            //route.ShapeMeta.Append(route1.ShapeMeta);
            //route.TotalTime += route1.TotalTime;
            //route.TotalDistance += route1.TotalDistance;

            var res = route.Concatenate(route1);

            return res;
        }
    }
}
