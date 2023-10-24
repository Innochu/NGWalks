using FluentValidation;
using NGWalksDomain.ModelDTO;

namespace NGWalksValidations.DTOFluentValidations
{
	public class RegisterDTOValidations : AbstractValidator<RegisterRequestDTO>
	{
        public RegisterDTOValidations()
        {
			RuleFor(x => x.UserName)
			.NotEmpty().WithMessage("UserName is required.")
			.EmailAddress().WithMessage("UserName should be a valid email address.");

			RuleFor(user => user.Password)
			.NotEmpty().WithMessage("Password is required.")
			.Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$")
			.WithMessage("Password must contain at least one " +
			"uppercase letter, one lowercase letter, one digit, and be at least 8 characters long.");

		}

    }
}
