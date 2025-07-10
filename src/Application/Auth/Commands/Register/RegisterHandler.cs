using Application.Contracts;
using Ardalis.Result;

namespace Application.Auth.Commands.Register;

internal class RegisterHandler(IUserService userService) : ICommandHandler<RegisterCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await userService.CreateFromRegistrationAsync(request.FirstName, request.LastName, request.UserName, request.Email, request.Password);
    }
}
