using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace API.Controllers;

public class ErrorController : BaseApiController
{
    private readonly AppDbContext _appDbContext;

    public ErrorController(AppDbContext appDbContext) {
        _appDbContext = appDbContext;
    }

    [Authorize]
    [HttpGet("auth-error")]
    public ActionResult<string> GetAuthentication() {

        return "Authentication error";
    }

    [HttpGet("server-error")]
    public ActionResult<ApplicationUser> GetServerError() {

        // null reference error
        var payload = _appDbContext.Users.Find(-1) ?? throw new Exception("Something bad happened on server");
        // will throw to CustomExceptionMiddleware

        return payload;
    }

    [HttpGet("not-found")]
    public ActionResult<ApplicationUser> GetNotFound() {

        var payload = _appDbContext.Users.Find(-1);
        
        if(payload == null) {
            return NotFound();
        }

        return payload;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest() {
        // public ActionResult<string> GetBadRequest()

        return BadRequest("Request is bad");
    }

    

    
}
