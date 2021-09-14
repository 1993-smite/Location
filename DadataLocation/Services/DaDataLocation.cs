using Dadata;
using Dadata.Model;
using DadataLocation.Models;
using DadataLocation.Services.Interfaces;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using SuggestClient = Dadata.SuggestClient;
using DadataPhone = Dadata.Model.Phone;
using Phone = DadataLocation.Models.Phone;

namespace DadataLocation
{
    public class DaDataLocationService : ILocationService, IIPLocationService, IPhoneService
    {
        private string _key = "9d035861f269ba41c82ce284ab4afd3b3979ba93";
        private string _secret = "705eccbd748bfd6ff04613e04e833a8c6c676262";
        private string _url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";
        private SuggestClient _api => new SuggestClient(_key);

        #region Location
        private IEnumerable<Location> getLocations(IEnumerable<Suggestion<Address>> addresses)
        {
            var locations = new List<Location>();
            foreach (var suggestion in addresses)
            {
                locations.Add(getLocation(suggestion));
            }
            return locations;
        }

        private Location getLocation(Suggestion<Address> location)
        {
            Location locationResult = new Location();
            locationResult.Address = location.value;
            locationResult.Lat = location.data.geo_lat.ToDouble();
            locationResult.Lon = location.data.geo_lon.ToDouble();
            return locationResult;
        }

        public IEnumerable<Location> GeoCoding(double lat, double lon)
        {
            var response = _api.Geolocate(lat: lat, lon: lon);
            return getLocations(response.suggestions);
        }

        public IEnumerable<Location> GeoCoding(string address)
        {
            var response = _api.SuggestAddress(address);

            return getLocations(response.suggestions);
        }

        public Location GeoCodingByIp(string ip)
        {
            var response = _api.Iplocate(ip);

            return getLocation(response.location);
        }
        #endregion

        #region Phone

        public async Task<Phone> GetPhone(string number)
        {
            var api = new CleanClientAsync(_key, _secret);
            var result = await api.Clean<DadataPhone>(number);

            Location location = new Location();
            location.Address = $"{result.country},{result.region},{result.city},{result.provider}";

            return new PhoneLocation(result.phone, location);
        }

        #endregion
    }
}
