using Accord.MachineLearning;
using Itinero;
using LocationOsmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Services.ClusterizationService
{
    public class KmeansClusterize : IClusterize
    {
        public IEnumerable<IEnumerable<Place>> Clusterize(int count, IList<Place> places)
        {
            var coords = places
                .AsParallel()
                .Select(x => new double[2] { x.Latitute, x.Longitude })
                .ToArray();

            var kmeans = new KMeans(k: count);
            var clusters = kmeans.Learn(coords).Decide(coords);

            var placeClusters = new List<List<Place>>(count);

            for(int index = 0;index < clusters.Length; index++)
            {
                var cluster = clusters[index];
                if (placeClusters[cluster] is null)
                    placeClusters[cluster] = new List<Place>();

                placeClusters[cluster].Add(places[index]);
            }


            return placeClusters;
        }
    }
}
