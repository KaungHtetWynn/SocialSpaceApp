using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    // Table name will be Users
    // For navigation property, it will use the name of entity, so Image
    // If you want custom table name use [Table("Images")]
    public DbSet<ApplicationUser> Users { get; set; }
}
