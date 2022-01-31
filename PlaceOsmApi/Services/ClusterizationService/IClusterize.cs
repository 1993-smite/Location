using LocationOsmApi.Models;
using System.Collections.Generic;

namespace PlaceOsmApi.Services.ClusterizationService
{
    interface IClusterize
    {
        IEnumerable<IEnumerable<Place>> Clusterize(int count, IList<Place> places);
    }
}
