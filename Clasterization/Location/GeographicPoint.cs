using Clasterization.Location;

namespace Clasterization
{
    public class GeographicPoint : IPoint
    {
        public const double R = 6371000;

        public int Id { get; }
        public double Lat { get; }
        public double Lon { get; }

        public double[] Coordinates()
        {
            return new[] { Lat, Lon };
        }

        public GeographicPoint(int id, double lat, double lon)
        {
            Id = id;
            Lat = lat;
            Lon = lon;
        }

    }
}
