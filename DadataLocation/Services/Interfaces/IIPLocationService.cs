using DadataLocation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DadataLocation.Services.Interfaces
{
    interface IIPLocationService
    {
        /// <summary>
        /// get location by ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        Location GeoCodingByIp(string ip);
    }
}
