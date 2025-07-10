using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models;

public class ApplicationUser : IdentityUser<Guid>, IAuditableEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
}
