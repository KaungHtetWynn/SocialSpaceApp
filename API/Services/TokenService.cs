using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.IServices;
using API.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(AppUser user)
    {
        // Token Key Validation: Checks if the token key is present and of sufficient length.
        var tokenKey = _configuration["TokenKey"] ?? throw new Exception("TokenKey cannot be accessed from appsettings");

        // Token key length must be greater than 64 because of HmacSha256Signature
        if (tokenKey.Length < 64)
        {
            throw new Exception("Token key needs to be longer");
        }

        // Key Creation: Creates a symmetric security key from the token key.
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        // Constructs a list of claims for the JWT payload
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName)
        };

        // Specify the algorithm that we want to encrypt this key with.
        // Creates signing credentials using the symmetric security key
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        // Defines the JWT token descriptor with claims, expiration (7 days from now), and signing credentials
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials
        };

        // Uses JwtSecurityTokenHandler to create and write the JWT token
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }










}
