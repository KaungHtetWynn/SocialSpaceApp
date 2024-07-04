namespace API.Models;

public class AppUser
{
    // Default Id Column in EF Core is Id
    // If you want UserId then you need to decorate it with [Key] attribute
    public int Id { get; set; }
    // .net identity uses this casing UserName but its optional
    public required string UserName { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}
