using API.Data;
using API.IServices;
using API.Repository;
using API.Repository.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

// Current we are not using this extension
// We commented out in the Program.cs, we are directly registering services in the Program.cs file
public static class AppServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddControllers();

// Register the service with DI container
services.AddDbContext<AppDbContext>(options =>
{
    // Options that we are going to pass to the DbContext
    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

services.AddCors();
// Specify the lifetime of a service.
services.AddScoped<ITokenService, TokenService>();


return services;
        }
}
