using API.Data;
using API.DTOs;
using API.Models;
using API.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// Inherits BaseApiController

[Authorize]
public class UsersController: BaseApiController
{
    private readonly IApplicationUserRepository _applicationUserRepository;


    // ApplicationUserRepository, code changed (now mapping at a repository level or database level, so no need to inject IMapper)
    //private readonly IMapper _mapper;

    public UsersController(IApplicationUserRepository applicationUserRepository, IMapper mapper)
    {
        _applicationUserRepository = applicationUserRepository;
        //_mapper = mapper;
    }

    //[AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers() {

        // await the part of the code that has the potential to block our requests
        // users are IEnumerable<ApplicationUser>
        //var users = await _applicationUserRepository.GetAllUsers();
        var users = await _applicationUserRepository.GetAllMembers();

        // using AutoMapper to map ApplicationUser to MemberDto
        //var usersMapped = _mapper.Map<IEnumerable<MemberDto>>(users);

        //now mapping at a repository level

        // returning http response
        // we can return different types of http response because of ActionResult
        return Ok(users);
    }

    //[Authorize]
    //[HttpGet("{id}")]
    //[HttpGet("{id:int}")] // type safety for route parameter
    [HttpGet("{username}")] // /api/users/username
    public async Task<ActionResult<MemberDto>> GetUser(string username) {

        //var user = await _applicationUserRepository.GetUserByUsername(username); // ApplicationUser
        var user = await _applicationUserRepository.GetMemberByUsername(username);
        
        if(user == null) {
            return NotFound();
        }

        //var userMapped = _mapper.Map<MemberDto>(user); // We don't need this anymore because now we are mapped at repository/database level

        //return Ok(userMapped);
        return user;
    }
}
