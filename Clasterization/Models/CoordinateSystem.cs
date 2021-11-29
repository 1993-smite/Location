using System;
using System.Collections.Generic;
using System.Text;

namespace Clasterization.Location
{
    public static class CoordinateSystem
    {
        public static GeographicPoint toGeographic(this CartesianPoint point)
        {
            double lon = Math.Acos(point.Z / GeographicPoint.R);
            double lat = Math.Atan(point.Y / point.X);
            return new GeographicPoint(point.Id,lat, lon);
        }

        public static GeographicPoint toGeographic(this PolarPoint point) => point.toCartesian().toGeographic();

        public static CartesianPoint toCartesian(this GeographicPoint point)
        {
            double x = GeographicPoint.R * Math.Cos(point.Lat) * Math.Cos(point.Lon);
            double y = GeographicPoint.R * Math.Cos(point.Lat) * Math.Sin(point.Lon);
            double z = GeographicPoint.R * Math.Sin(point.Lat);

            return new CartesianPoint(point.Id, x, y, z);
        }

        public static CartesianPoint toCartesian(this PolarPoint point)
        {
            double x = point.D * Math.Cos(point.Angle);
            double y = point.D * Math.Sin(point.Angle);

            return new CartesianPoint(point.Id, x, y, GeographicPoint.R);
        }
    }
}
