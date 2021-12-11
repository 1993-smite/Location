using LocationOsmApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Models
{
    public class RouteMap : IEnumerable<RouteMapStep>
    {
        public IList<RouteMapStep> Places { get; private set; }

        public IEnumerator<RouteMapStep> GetEnumerator()
        {
            foreach(var place in Places)
            {
                yield return place;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RouteMapStep> Add(RouteMapStep place)
        {
            Places.Add(place);
            return Places;
        }

        public IEnumerable<RouteMapStep> Remove(Func<RouteMapStep, bool> func)
        {
            Places = Places.Where(func).ToList();
            return Places;
        }
    }
}
