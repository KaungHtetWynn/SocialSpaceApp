using API.Extensions;

namespace API.Models;

public class ApplicationUser
{
    // Default Id Column in EF Core is Id
    // If you want UserId then you need to decorate it with [Key] attribute
    public int Id { get; set; }
    // .net identity uses this casing UserName but its optional
    public required string UserName { get; set; }

    // we will be seeding our user in different way
    // to prevent compiler we remove required and initialize it empty array
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    // required means you must initialize when you create this object
    public required string FullName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public required string Gender { get; set; }
    public string? MaritialStatus { get; set; }
    public string? Occupation { get; set; }
    public string? Description { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    // Navigation property
    // No need DbSet for Photo, EF will create the appropriate table
    public List<Photo> Photos { get; set; } = [];

    // AutoMapper query issue
    // public int GetAge()
    // {
    //     return DateOfBirth.CalculateAge();
    // }
}

