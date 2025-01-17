using System.Text;
using API.Data;
using API.Extensions;
using API.IServices;
using API.Middleware;
using API.Repository;
using API.Repository.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (Registering our services)

builder.Services.AddControllers();

// Register the service with DI container
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Options that we are going to pass to the DbContext
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddCors();
// Specify the lifetime of a service.
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

// tell Automapper where to find the code (AutoMapperProfiles file)
// register AutoMapper as a service so that we can inject it
// Registers AutoMapper, scans the assembly for mappings and profiles
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// // JwtBearerDefaults -> Microsoft.AspNetCore.Authentication.JwtBearer
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         // Need the same token key to decrypt and to encrypt when we create token
//         var tokenKey = builder.Configuration["TokenKey"] 
//             ?? throw new Exception("Token key not found");
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             // Only accepts signed token (not any token)
//             ValidateIssuerSigningKey = true,
//             // validating against ssk
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
//             ValidateIssuer = false,
//             ValidateAuience = false
//         };
//     });

// Services type is IServiceCollection
// *** Custom extensions methods ***
//builder.Services.AddAppServices(builder.Configuration); // See AppServiceExtensions file
builder.Services.AddIdentityServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline. (request processing pipeline)
// middlewares (order is important)
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// error handling middleware or exception handling middleware has to go at the top of the pipeline.
app.UseMiddleware<CustomExceptionMiddleware>();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

// authenticate someone before authorize them.
app.UseAuthentication();
app.UseAuthorization();

// Map controllers endpoints
app.MapControllers();

// apply migration and seed the data
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<AppDbContext>();
    await context.Database.MigrateAsync();
    await Seeder.SeedUsers(context);
}
catch (Exception ex)
{

    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex,"Error occurred during migration");
}

app.Run();
