using FluentValidation;

namespace Application.Auth.Commands.Refresh;

public class RefreshValidator : AbstractValidator<RefreshCommand>
{
    public RefreshValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty()
            .MaximumLength(256);
    }
}
