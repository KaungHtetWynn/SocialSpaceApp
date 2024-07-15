using System.Net;
using System.Text.Json;
using API.ErrorHandling;

namespace API.Middleware;

// new way to do constructor injection
public class CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger,
       IHostEnvironment env)
{

    // To see whether we are in production or development env
    // public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger,
    //     IHostEnvironment env)
    // {

    // }

    // When we're using middleware we have to call the invoke async because that is what is being expected 
    // when we call the next method that's going to look for an invoke async method.
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // We're simply going to move on to the next request delegate.
            // We're only handling errors inside here, and we're not doing anything with the HTTP request itself 
        Console.WriteLine("No Exception Occured ***");
            await next(context);
        }
        catch (Exception ex)
        {
            // log to console
            //logger.LogError(ex, ex.Message);
            Console.WriteLine("Exception Occured ***");

            // http response
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

            // return as json response so client can use
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }


}
