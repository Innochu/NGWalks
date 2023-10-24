using FluentValidation;
using FluentValidation.AspNetCore;
using NGWalksDomain.ModelDTO;

namespace NGWalksValidations.DTOFluentValidations
{
    public class CreateRegionDTOValidation : AbstractValidator<CreateRegionDTO>
    {
        public CreateRegionDTOValidation()
        {
            RuleFor(region => region.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(20).WithMessage("Name cannot exceed 20 characters.")
            .Must(name => !name.Contains(" ")).WithMessage("Name cannot contain white spaces.");



            RuleFor(region => region.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MinimumLength(3).WithMessage("Code must be at least 3 characters long.")
            .MaximumLength(3).WithMessage("Code must not exceed 3 characters.");

            RuleFor(region => region.RegionImageUrl)
           .NotEmpty().WithMessage("RegionImageUrl is required.");
        }
    }
}
