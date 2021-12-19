using NUnit.Framework;
using PlaceOsmApi.Services;
using System;

namespace PlaceOsmApi.Test
{
    [TestFixture]
    public class DatataServiceTest
    {
        public const string DadataToken = "9d035861f269ba41c82ce284ab4afd3b3979ba93";

        private Lazy<DadataService> dadataService = new Lazy<DadataService>(() => new DadataService(DadataToken));
        private DadataService GetDadataService => dadataService.Value;

        [TestCase("г Москва, метро Комсомольская (Кольцевая)", 55.775672, 37.654772)]
        [TestCase("г Москва, метро Киевская (Кольцевая)", 55.74361, 37.56735)]
        public void GetPlaceByAddressTest(string address, double lat = 0, double lon = 0)
        {
            var place = GetDadataService.GetPlaceByAddress(address);

            Assert.AreEqual(address, place.Address);
            Assert.AreEqual(lat, place.Latitute);
            Assert.AreEqual(lon, place.Longitude);
        }

        [TestCase(55.775672, 37.654772, "г Москва, метро Комсомольская (Кольцевая)")]
        [TestCase(55.74361, 37.56735, "г Москва, метро Киевская (Кольцевая)")]
        public void GetPlaceByGeoTest(double lat = 0, double lon = 0, string address = "")
        {
            var place = GetDadataService.GetPlaceByGeo(lat, lon);

            Assert.AreEqual(address, place.Address);
            Assert.AreEqual(lat, place.Latitute);
            Assert.AreEqual(lon, place.Longitude);
        }
    }
}