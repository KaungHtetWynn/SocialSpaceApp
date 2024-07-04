using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// Inherits BaseApiController
public class UsersController: BaseApiController
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() {

        // await the part of the code that has the potential to block our requests
        var users = await _context.Users.ToListAsync();

        // returning http response
        // we can return different type of http response because of ActionResult
        //return Ok(users);
        return users;
        
    }

    [Authorize]
    //[HttpGet("{id}")]
    [HttpGet("{id:int}")] // type safety
    public async Task<ActionResult<AppUser>> GetUser(int id) {

        var user = await _context.Users.FindAsync(id);
        
        if(user != null)
            return user;

        return NotFound();
    }
}
