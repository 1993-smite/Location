namespace LocationOsmApi.Models
{
    /// <summary>
    /// place geo and address + country, street...
    /// </summary>
    public class Place
    {
        /// <summary>
        /// country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// city
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// street
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// home
        /// </summary>
        public string Home { get; set; }
        /// <summary>
        /// address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// lat
        /// </summary>
        public double? Lat { get; set; }
        /// <summary>
        /// auto prop latitute
        /// </summary>
        public double Latitute => Lat ?? 0;
        /// <summary>
        /// lon
        /// </summary>
        public double? Lon { get; set; }
        /// <summary>
        /// auto prop longitude 
        /// </summary>
        public double Longitude => Lon ?? 0;
        /// <summary>
        /// ExistCoordinates
        /// </summary>
        public bool ExistCoordinates => !(Lat == null || Lon == null);

        /// <summary>
        /// constructor
        /// </summary>
        public Place()
        {

        }

        /// <summary>
        /// constructor with address
        /// </summary>
        /// <param name="address"></param>
        public Place(string address): this()
        {
            Address = address;
        }

        /// <summary>
        /// constructor with lat and lon
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        public Place(double lat, double lon) : this()
        {
            Lat = lat;
            Lon = lon;
        }

        /// <summary>
        /// constructor with address and lat and lon
        /// </summary>
        /// <param name="address"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        public Place(string address, double lat, double lon): this(lat, lon)
        {
            Address = address;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="address"></param>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="street"></param>
        /// <param name="home"></param>
        public Place(string address, double lat, double lon, string country, string city, string street, string home): this(address, lat, lon)
        {
            Country = country;
            City = city;
            Street = street;
            Home = home;
        }
    }
}
