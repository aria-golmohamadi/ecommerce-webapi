using Application.Contracts;
using Ardalis.Result;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services;

internal class UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IUserService
{
    public async Task<Result<bool>> CheckCredentialsAsync(string email, string password)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Result<bool>.Unauthorized("The credentials you entered are incorrect.");
        }

        bool passwordCheckResult = await userManager.CheckPasswordAsync(user, password);

        if (!passwordCheckResult)
        {
            return Result<bool>.Unauthorized("The credentials you entered are incorrect.");
        }

        return Result<bool>.Success(true, "The credentials you entered are correct.");
    }

    public async Task<Result<bool>> CreateFromRegistrationAsync(string firstName, string lastName, string userName, string email, string password)
    {
        ApplicationUser user = new()
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = userName,
            Email = email
        };

        IdentityResult userCreationResult = await userManager.CreateAsync(user, password);
    
        if (!userCreationResult.Succeeded)
        {
            return Result<bool>.Invalid(userCreationResult.Errors.Select(x => new ValidationError(x.Code, x.Description)));
        }

        IdentityResult roleAssignmentResult = await userManager.AddToRoleAsync(user, "User");

        if (!roleAssignmentResult.Succeeded)
        {
            return Result<bool>.Error(new ErrorList(roleAssignmentResult.Errors.Select(x => x.Description)));
        }

        return Result<bool>.Success(true, "Your account has been created successfully.");
    }

    public async Task<Result<Guid>> RetrieveIdAsync(string email)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Result<Guid>.NotFound("We couldn’t find any user with that email.");
        }

        return Result<Guid>.Success(user.Id, "The requested id retrieved successfully.");
    }

    public async Task<Result<bool>> SignInAsync(string email, bool persistent = true)
    {
        ApplicationUser? user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return Result<bool>.NotFound("We couldn’t find any user with that email.");
        }

        await signInManager.SignInAsync(user, persistent);

        return Result<bool>.Success(true, "You have successfully logged in.");
    }
}
