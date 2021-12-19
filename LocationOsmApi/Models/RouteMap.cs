using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PlaceOsmApi.Models
{
    /// <summary>
    /// route map
    /// </summary>
    public class RouteMap : IEnumerable<RouteMapStep>
    {
        /// <summary>
        /// places
        /// </summary>
        public IList<RouteMapStep> Places { get; private set; }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<RouteMapStep> GetEnumerator()
        {
            foreach(var place in Places)
            {
                yield return place;
            }
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// add step
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public IEnumerable<RouteMapStep> Add(RouteMapStep place)
        {
            Places.Add(place);
            return Places;
        }

        /// <summary>
        /// remove by func<step,bool>
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public IEnumerable<RouteMapStep> Remove(Func<RouteMapStep, bool> func)
        {
            Places = Places.Where(func).ToList();
            return Places;
        }
    }
}
