using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Models;
using API.Repository;
using API.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [ApiController]
// [Route("api/[controller]")]
// Inherits BaseApiController

[Authorize] // Bearer token will be send by client everytime the request comes into this controller
public class UsersController: BaseApiController
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IMapper _mapper;

    // ApplicationUserRepository, code changed (now mapping at a repository level or database level, so no need to inject IMapper)
    //private readonly IMapper _mapper;

    public UsersController(IApplicationUserRepository applicationUserRepository, IMapper mapper)
    {
        _applicationUserRepository = applicationUserRepository;
        _mapper = mapper;
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

    // return ActionResult whether update is successful
    [HttpPut]
    public async Task<ActionResult> UpdateUser (MemberUpdateDto memberUpdateDto) {
        // Bearer token will be send by client everytime the request comes into this controller
        // We can use the User claim in this incoming request to retrieve of username
        // get hold of username from http context
        // Check TokenService we used UserName as name identifier
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // if FindFirst not return null get its string value 

        if(username == null) {
            return BadRequest("Username not found inside token");
        }

        // ef tracking user
        var user = await _applicationUserRepository.GetUserByUsername(username);

        if(user == null) {
            return BadRequest("User not found");
        }

        // Automapper
        // Mapping MemberUpdateDto to Application User
        // changes will be saved by ef 
        _mapper.Map(memberUpdateDto, user);

        // or You can explicitly tell to update instead of relying on ef tracking
        // explicitly telling user has been modified
        //_applicationUserRepository.UpdateUser(user);

        // ef tracking will not save the data again, if there is no changes in memberUpdateDto meaning if we submit second time after first time,
        // first time will return nocontent and second time will return "User update failed" which is badrequest
        // if we submit without changes again, there will be no changes so it will return BadRequest
        if(await _applicationUserRepository.SaveAll()) {
            return NoContent();
        }

        return BadRequest("User update failed");
    }
}
