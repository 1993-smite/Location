namespace LocationOsmApi.Models
{
    public class Place
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Home { get; set; }
        public string Address { get; set; }
        public double? Lat { get; set; }
        public double Latitute => Lat ?? 0;
        public double? Lon { get; set; }
        public double Longitude => Lon ?? 0;

        public bool ExistCoordinates => !(Lat == null || Lon == null);

        public Place()
        {

        }

        public Place(string address): this()
        {
            Address = address;
        }

        public Place(double lat, double lon) : this()
        {
            Lat = lat;
            Lon = lon;
        }

        public Place(string address, double lat, double lon): this(lat, lon)
        {
            Address = address;
        }

        public Place(string address, double lat, double lon, string country, string city, string street, string home): this(address, lat, lon)
        {
            Country = country;
            City = city;
            Street = street;
            Home = home;
        }
    }
}
