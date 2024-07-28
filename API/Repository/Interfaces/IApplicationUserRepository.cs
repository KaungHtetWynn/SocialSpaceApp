using API.DTOs;
using API.Models;

namespace API.Repository.Interfaces;

// method signatures in interface are public by default
public interface IApplicationUserRepository
{
    Task<bool> SaveAll();
    Task<ApplicationUser?> GetUserById(int id);
    Task<ApplicationUser?> GetUserByUsername(string username);
    void UpdateUser(ApplicationUser user);
    Task<IEnumerable<ApplicationUser>> GetAllUsers();
    Task<MemberDto?> GetMemberByUsername(string username);
    Task<IEnumerable<MemberDto>> GetAllMembers();
     
}
