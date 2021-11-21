using Accord.MachineLearning;
using Accord.Math.Distances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clasterization.Location
{
    public enum ClusterizationMode
    {
        Kmeans,
        BinarySplit,
        MeanSplit,
        Gausian
    }

    public class Clusterization<T> where T : IPoint
    {
        public ClusterizationMode Mode { private set; get; }

        public Clusterization(ClusterizationMode mode = ClusterizationMode.Kmeans)
        {
            Mode = mode;
        }

        private int[] kMeansClusterization(int count, double[][] coords)
        {
            int[] labels = new int[coords.Length];
            KMeansClusterCollection clusters;
            switch (Mode)
            {
                case ClusterizationMode.Kmeans:
                    var kmeans = new KMeans(count)
                    {
                        Distance = new Euclidean(),

                        // We will compute the K-Means algorithm until cluster centroids
                        // change less than 0.5 between two iterations of the algorithm
                        Tolerance = 0.05
                    };
                    clusters = kmeans.Learn(coords);
                    labels = clusters.Decide(coords);
                    break;
                case ClusterizationMode.BinarySplit:
                    var binarySplit = new BinarySplit(count);
                    clusters = binarySplit.Learn(coords);
                    labels = clusters.Decide(coords);
                    break;
                case ClusterizationMode.MeanSplit:
                    var meanShift = new MeanShift();
                    var res = meanShift.Learn(coords);
                    labels = res.Decide(coords);
                    break;
                case ClusterizationMode.Gausian:
                    var gaussianMixture = new GaussianMixtureModel(count);
                    var result = gaussianMixture.Learn(coords);
                    labels = result.Decide(coords);
                    break;
            }
            return labels;
        }

        public IEnumerable<Cluster<T>> Clusterize(IEnumerable<T> items, int countClusters = 0)
        {
            double[][] coords = new double[items.Count()][];
            for(int index = 0; index < items.Count(); index++) 
            {
                coords[index] = items.ElementAt(index).Coordinates();
            }

            var clusters = kMeansClusterization(countClusters, coords);

            var mx = clusters.Max();
            var result = new List<Cluster<T>>();

            for (int index = 0; index <= mx; index++)
                result.Add(new Cluster<T>());

            for(int index = 0; index < clusters.Length; index++)
            {
                var cluster = clusters[index];
                result[cluster].Items.Add(items.ElementAt(index));
            }

            return result;
        }
    }
}
