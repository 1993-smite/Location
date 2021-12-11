using FluentValidation;
using LocationOsmApi.Models;

namespace LocationOsmApi.Validators
{
    public class PlaceValidator: AbstractValidator<Place>
    {
        public PlaceValidator()
        {
            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .When(x => !x.Lat.HasValue && !x.Lon.HasValue);
            RuleFor(x => x.Lat)
                .NotNull()
                .When(x => !x.Lon.HasValue);
            RuleFor(x => x.Lon)
                .NotNull()
                .When(x => !x.Lat.HasValue);
        }
    }
}
