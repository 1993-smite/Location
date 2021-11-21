using System;
using System.Collections.Generic;
using System.Text;

namespace Clasterization.Location
{
    public class PolarPoint: IPoint
    {
        public int Id { get; }
        public double D { get; }
        public double Angle { get; }

        public double[] Coordinates()
        {
            return new[] { D, Angle };
        }

        public PolarPoint(int id, double d, double angle)
        {
            this.Id = id;
            this.D = d;
            this.Angle = angle;
        }
    }
}
