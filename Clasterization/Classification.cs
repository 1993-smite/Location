using Accord.MachineLearning.Bayes;
using Accord.Statistics.Distributions.Univariate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clasterization
{
    public static class Classification
    {
        public static void Compute(double[][] inputs)
        {
            var teacher = new NaiveBayesLearning<NormalDistribution>();

            // Use the learning algorithm to learn
            var nb = teacher.Learn(inputs, inputs);

            // At this point, the learning algorithm should have
            // figured important details about the problem itself:
            int numberOfClasses = nb.NumberOfClasses; // should be 2 (positive or negative)
            int nunmberOfInputs = nb.NumberOfInputs;  // should be 2 (x and y coordinates)

            // Classify the samples using the model
            int[] answers = nb.Decide(inputs);
        } 
    }
}
