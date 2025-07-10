using Application.Contracts;
using Ardalis.Result;

namespace Application.Auth.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string UserName, string Email, string Password) : ICommand<Result<bool>>;
