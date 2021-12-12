using Dadata;
using Dadata.Model;
using LocationOsmApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Services
{
    public class DadataService: IGeoLocationService
    {
        private Lazy<SuggestClientAsync> lazyClientAsync;
        private SuggestClientAsync clientAsync => lazyClientAsync.Value;

        public DadataService(string token)
        {
            lazyClientAsync = new Lazy<SuggestClientAsync>(()=> new SuggestClientAsync(token));
        }

        public async Task<Place> GetPlaceByAddressAsync(string address)
        {
            var response = await clientAsync.SuggestAddress(address, 1);
            return response.ToPlace();
        }

        public Place GetPlaceByAddress(string address)
        {
            var tsk = GetPlaceByAddressAsync(address);
            tsk.Wait();
            return tsk.Result;
        }

        public async Task<Place> GetPlaceByGeoAsync(double lat, double lon)
        {
            var response = await clientAsync.Geolocate(lat, lon);
            return response.ToPlace();
        }

        public Place GetPlaceByGeo(double lat, double lon)
        {
            var tks = GetPlaceByGeoAsync(lat, lon);
            tks.Wait();
            return tks.Result;
        }

    }

    public static class DadataExtensions
    {
        public static Place ToPlace(this SuggestResponse<Address> response)
        {
            var suggestion = response.suggestions.FirstOrDefault();

            double.TryParse(suggestion.data.geo_lat.Replace('.',','), out double lat);
            double.TryParse(suggestion.data.geo_lon.Replace('.', ','), out double lon);

            return new Place(suggestion.value, lat, lon);
        }
    }
}
