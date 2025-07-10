using FluentValidation;

namespace Application.Auth.Commands.LoginWithCookie;

public class LoginWithCookieValidator : AbstractValidator<LoginWithCookieCommand>
{
    public LoginWithCookieValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(256)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
