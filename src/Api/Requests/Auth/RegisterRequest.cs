using System.ComponentModel.DataAnnotations;

namespace Api.Requests.Auth;

public class RegisterRequest
{
    [Required]
    [MaxLength(256)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}
