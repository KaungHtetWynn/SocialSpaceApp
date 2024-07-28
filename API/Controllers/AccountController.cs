using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.IServices;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AccountController(AppDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")] // api/account/register [don't confuse with (Name = "GetWeatherForecast")]
    public async Task<ActionResult<UserDto>> Register(RegistrationDto registrationDto)
    {
        return Ok();
        // if (await CheckIfUserExists(registrationDto.UserName))
        // {
        //     return BadRequest("Username already exists");
        // }

        // // return Ok();

        // // And once this class is out of scope, as in it's not being used anymore, 
        // // then the dispose method will be called and it will be disposed of.
        // using var hmacAuth = new HMACSHA512(); // use to encrypt text
        // // create instance of HMACSHA512

        // var user = new ApplicationUser
        // {
        //     UserName = registrationDto.UserName.ToLower(),
        //     PasswordHash = hmacAuth.ComputeHash(Encoding.UTF8.GetBytes(registrationDto.Password)), // create byte array from password and compute hash
        //     PasswordSalt = hmacAuth.Key // salt our password
        // };

        // _context.Users.Add(user);
        // await _context.SaveChangesAsync();

        // var token = _tokenService.CreateToken(user);
        // var loggedInUser  = new UserDto {
        //     UserName = user.UserName,
        //     Token = token
        // };

        // return loggedInUser;
    }

    [HttpPost("login")] // give route parameter as login
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {

        var user = await _context.Users.Where(user => user.UserName == loginDto.UserName.ToLower())
                    .FirstOrDefaultAsync();

        if (user == null)
        {
            return Unauthorized("Username is invalid");
        }

        using var hmacAuth = new HMACSHA512(user.PasswordSalt);

        var loginPasswordHash = hmacAuth.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < loginPasswordHash.Length; i++)
        {
            if (loginPasswordHash[i] != user.PasswordHash[i])
            {
                return Unauthorized("Password is invalid");
            }
        }

        var token = _tokenService.CreateToken(user);
        var loggedInUser  = new UserDto {
            UserName = user.UserName,
            Token = token
        };

        return loggedInUser;
    }

    public async Task<bool> CheckIfUserExists(string userName)
    {

        return await _context.Users.AnyAsync(user => user.UserName.ToLower() == userName.ToLower());
    }
}
