using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seeder
{
    public static async Task SeedUsers(AppDbContext context)
    {
        if (await context.Users.AnyAsync())
        {
            return;
        }

        var applicationUserData = await File.ReadAllTextAsync("Data/ApplicationUserSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // make sure 
        var users = JsonSerializer.Deserialize<List<ApplicationUser>>(applicationUserData, options);

        if (users == null)
        {
            return;
        }

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();

            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;

            context.Users.Add(user);

        }

        await context.SaveChangesAsync();

    }
}
