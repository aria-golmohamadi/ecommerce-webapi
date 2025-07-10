using FluentValidation;

namespace Application.Auth.Commands.LoginWithJwt;

public class LoginWithJwtValidator : AbstractValidator<LoginWithJwtCommand>
{
    public LoginWithJwtValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
