using System.Collections.Generic;

namespace DadataLocation
{
    /// <summary>
    /// location
    /// </summary>
    public struct Location
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Address { get; set; }
    }


    interface ILocationService
    {
        /// <summary>
        /// back geo location
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        IEnumerable<Location> GeoCoding(double lat, double lon);

        /// <summary>
        /// geo location
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        IEnumerable<Location> GeoCoding(string address);
    }
}
