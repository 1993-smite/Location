using LocationOsmApi.Models;
using NUnit.Framework;
using Osrm.Client;
using System;
using System.Net.Http;
using Osrm.Client.Models;

namespace PlaceOsmApi.Test
{
    [TestFixture]
    class OsrmServiceTest
    {
        public const string OsrmApiLink = "http://router.project-osrm.org/";
        private Lazy<Osrm5x> lazyOsrm5X = new Lazy<Osrm5x>(()=> new Osrm5x(new HttpClient(), OsrmApiLink));
        private Osrm5x osrm5X => lazyOsrm5X.Value;

        [TestCase(55.775672, 37.654772, 55.74361, 37.56735)]
        public async void TableTest(double lat1, double lon1, double lat2, double lon2)
        {
            var tbl = await osrm5X.Table(new Location(lat1, lon2), new Location(lat2, lon2));

            Assert.AreEqual(tbl.Destinations.Length, 2);
            Assert.AreEqual(tbl.Durations.Length, 2);

            Assert.AreEqual(tbl.Durations[0][0], 0);
            Assert.AreEqual(tbl.Durations[1][1], 0);
        }
    }
}
