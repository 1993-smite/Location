using DadataLocation.Models;
using System.Collections.Generic;

namespace DadataLocation.Services.Interfaces
{
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
