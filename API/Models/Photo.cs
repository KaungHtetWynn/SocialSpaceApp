using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

// EFCore will generate Photos table
[Table("Photos")]
public class Photo
{
    // PK
    public int Id { get; set; }
    public required string ImageUrl { get; set; }

    public bool IsProfilePhoto { get; set; }

    // CloudId
    public string? PublicId { get; set; }

    // Navigation properties
    // Setting required one to many relationship
    // Otherwise you will saving a Photo without any association to a user.
    public int ApplicationUserId { get; set; } // Optional foreign key property
    
    // Optional reference navigation to principal
    public ApplicationUser ApplicationUser { get; set; } = null!; // null forgiving operator
}
