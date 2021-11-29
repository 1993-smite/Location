using System;
using System.Collections.Generic;
using System.Text;

namespace Clasterization.Location
{
    public class RelativeGeographicPoint: IPoint
    {
        public CartesianPoint Base { get; }

        public CartesianPoint Point { get; }
        private GeographicPoint GeoPoint { get; }
        public GeographicPoint GeographicPoint() 
        {
            return GeoPoint != null 
                    ? GeoPoint
                    : new CartesianPoint(Point.Id
                        , Base.X + Point.X
                        , Base.Y + Point.Y
                        , Base.Z + Point.Z)
                        .toGeographic();
        }

        public double[] Coordinates()
        {
            double k = 1;
            return new double[] { Point.X * k, Point.Y * k, Point.Z * k };
        }

        public RelativeGeographicPoint(CartesianPoint basePoint, CartesianPoint point)
        {
            Base = basePoint;
            var geo = new CartesianPoint(point.Id
                , point.X - basePoint.X
                , point.Y - basePoint.Y
                , point.Z - basePoint.Z);


            this.Point = geo;
        }

        public RelativeGeographicPoint(CartesianPoint basePoint, GeographicPoint point)
        {
            Base = basePoint;
            GeoPoint = point;

            var pnt = point.toCartesian();
            var geo = new CartesianPoint(point.Id
                , pnt.X - basePoint.X
                , pnt.Y - basePoint.Y
                , pnt.Z - basePoint.Z);


            this.Point = geo;
        }
    }
}
