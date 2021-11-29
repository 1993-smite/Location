using System;
using System.Collections.Generic;
using System.Text;

namespace Clasterization.Location
{
    public class CartesianPoint : IPoint
    {
        public int Id { get; }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public double[] Coordinates()
        {
            return new [] { X, Y, Z };
        }

        public CartesianPoint(int id,double x, double y, double z)
        {
            Id = id;
            X = x;
            Y = y;
            Z = z;
        }
    }
}
