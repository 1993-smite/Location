using Dadata;
using Dadata.Model;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using SuggestClient = Dadata.SuggestClient;

namespace DadataLocation
{
    public class DaDataLocationService : ILocationService
    {
        private string _key = "9d035861f269ba41c82ce284ab4afd3b3979ba93";
        private string _url = "https://suggestions.dadata.ru/suggestions/api/4_1/rs";
        private SuggestClient _api => new SuggestClient(_key);

        private IEnumerable<Location> getLocations(IEnumerable<Suggestion<Address>> addresses)
        {
            var locations = new List<Location>();
            Location location = new Location();
            foreach (var suggestion in addresses)
            {
                location.Address = suggestion.value;
                location.Lat = suggestion.data.geo_lat.ToDouble();
                location.Lon = suggestion.data.geo_lon.ToDouble();
                locations.Add(location);
            }
            return locations;
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
    }
}
