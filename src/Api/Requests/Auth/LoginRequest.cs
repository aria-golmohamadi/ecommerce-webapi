using System.ComponentModel.DataAnnotations;

namespace Api.Requests.Auth;

public class LoginRequest
{
    [Required]
    [MaxLength(256)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
