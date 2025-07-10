using Ardalis.Result;

namespace Application.Contracts;

public interface IUserService
{
    Task<Result<bool>> CheckCredentialsAsync(string email, string password);
    Task<Result<bool>> CreateFromRegistrationAsync(string firstName, string lastName, string userName, string email, string password);
    Task<Result<Guid>> RetrieveIdAsync(string email);
    Task<Result<bool>> SignInAsync(string email, bool persistent = true);
}
