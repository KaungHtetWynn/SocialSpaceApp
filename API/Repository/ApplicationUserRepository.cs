using API.Data;
using API.DTOs;
using API.Models;
using API.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Repository;

// new way of dependency injection - AppDbcontext
public class ApplicationUserRepository(AppDbContext context, IMapper mapper) : IApplicationUserRepository
{
    public async Task<MemberDto?> GetMemberByUsername(string username)
    {
        // ConfigurationProvider - provided/registered in Program.cs
        // location of our mapping configuration
        return await context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }
    public async Task<IEnumerable<MemberDto>> GetAllMembers()
    {
        return await context.Users
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
    {
        // EF is lazy by default, use Include to fill related entities
        return await context.Users.Include(x => x.Photos).ToListAsync();
        //return await context.Users.ToListAsync();
    }

    

    public async Task<ApplicationUser?> GetUserById(int id)
    {
        // ? means optional (null will be returned by FindAsync if not found)
        return await context.Users.FindAsync(id);
    }

    public async Task<ApplicationUser?> GetUserByUsername(string username)
    {
        //return await context.Users.SingleOrDefaultAsync(user => user.UserName == username);
        return await context.Users.Include(x => x.Photos).SingleOrDefaultAsync(user => user.UserName == username);
    }

    public async Task<bool> SaveAll()
    {
        // SaveChangesAsync returns integer
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateUser(ApplicationUser user)
    {
        context.Entry(user).State = EntityState.Modified;
    }
}
