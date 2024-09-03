using System.ComponentModel.DataAnnotations;

namespace findata_api.DTOs.Account;

public class LoginDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}