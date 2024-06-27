namespace API.Models;

public class AppUser
{
    // Default Id Column in EF Core is Id
    // If you want UserId then you need to decorate it with [Key] attribute
    public int Id { get; set; }
    public required string UserName { get; set; }
}
