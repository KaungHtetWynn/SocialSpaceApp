using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
// Derived from controller base so classes inherit from this baseapi also inherits controllerbase
// [controller] = placeholder will be replace by child controller prefix name
public class BaseApiController: ControllerBase
{

}


