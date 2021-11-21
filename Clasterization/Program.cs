using System;
using Accord.MachineLearning;
using Clasterization.Location;

namespace Clasterization
{
    class Program
    {
        static void Main(string[] args)
        {
            double[][] coords =
            {
                new double[] { 55.85676319, 37.4845868 },
                new double[] { 55.867663016, 37.5003915 },
                new double[] { 55.84313842,  37.50420646 },
                new double[] { 55.8398685,  37.485131778 },
                new double[] { 55.85403824,  37.4764119 },
                new double[] { 55.858398,  37.4998465 },
                new double[] { 55.854038,  37.505296 },

                new double[] { 55.7768485,  37.6342852 },
                new double[] { 55.783769,  37.6347178 },
                new double[] { 55.7911229,  37.649857 },
                new double[] { 55.777714,  37.6611039 },
                new double[] { 55.794583,  37.6818668 },
                new double[] { 55.798909,  37.6671598 },

                new double[] { 55.816016,  37.795341 },
                new double[] { 55.806297,  37.798118 },
                new double[] { 55.807685,  37.777754 },
                new double[] { 55.815090,  37.805986 },
                new double[] { 55.799354,  37.807837 },
            };


            var kmeans = new KMeans(k: 3);
            var clusters = kmeans.Learn(coords);
            var ggg = clusters.Decide(coords);


            Console.ReadLine();
        }
    }
}
