using System.ComponentModel.DataAnnotations;

namespace Api.Requests.Auth;

public class RefreshRequest
{
    [Required]
    [MaxLength(256)]
    public string RefreshToken { get; set; } = string.Empty;
}
