using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Identity.Models;

public class ApplicationRole : IdentityRole<Guid>, IAuditableEntity
{
    public ApplicationRole() { }

    public ApplicationRole(string name) : base(name) { }

    public DateTime CreatedOnUtc { get; set; }
    public DateTime UpdatedOnUtc { get; set; }
}
