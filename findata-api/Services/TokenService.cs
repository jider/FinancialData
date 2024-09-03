using findata_api.interfaces;
using findata_api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace findata_api.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]));


    public string CreateToken(AppUser appUser)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName),
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"],            
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}
