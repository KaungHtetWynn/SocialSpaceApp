using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegistrationDto
{
    [Required] // For validation - also effects toastr
    public string UserName { get; set; } = string.Empty;
    [Required]
    [StringLength(6, MinimumLength = 4)]
    public string Password { get; set; } = string.Empty;
}
