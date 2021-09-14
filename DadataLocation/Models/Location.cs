using System;
using System.Collections.Generic;
using System.Text;

namespace DadataLocation.Models
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
}
