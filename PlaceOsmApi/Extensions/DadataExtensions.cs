using Dadata.Model;
using LocationOsmApi.Models;
using PlaceOsmApi.Services;
using System.Linq;

namespace PlaceOsmApi.Extensions
{
    public static class DadataExtensions
    {
        /// <summary>
        /// SuggestResponse<Address> to Place
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Place ToPlace(this SuggestResponse<Address> response)
        {
            var suggestion = response.suggestions.FirstOrDefault();

            double.TryParse(suggestion.data.geo_lat.Replace('.', ','), out double lat);
            double.TryParse(suggestion.data.geo_lon.Replace('.', ','), out double lon);

            return new Place(suggestion.value
                , lat
                , lon
                , suggestion.data.country
                , suggestion.data.city
                , suggestion.data.street
                , suggestion.data.house);
        }

        /// <summary>
        /// load dadata service to get place
        /// </summary>
        /// <param name="place"></param>
        /// <param name="dadataService"></param>
        /// <returns></returns>
        public static Place Load(this Place place, DadataService dadataService)
        {
            Place result = place;
            if (string.IsNullOrEmpty(place.Address) && place.ExistCoordinates)
            {
                result = dadataService.GetPlaceByGeo(place.Latitute, place.Longitude);
            }
            if (!place.ExistCoordinates && !string.IsNullOrEmpty(place.Address))
            {
                result = dadataService.GetPlaceByAddress(place.Address);
            }

            return result;
        }
    }
}
