using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegistrationDto
{
    [Required] // For validation
    public required string UserName { get; set; }
    [Required]
    public required string Password { get; set; }
}
