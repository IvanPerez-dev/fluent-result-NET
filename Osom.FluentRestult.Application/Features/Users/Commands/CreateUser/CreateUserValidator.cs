using FluentValidation;

namespace Osom.FluentRestult.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
