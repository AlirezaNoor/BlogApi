using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLG.Infrastructure.customRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLG.Services.Customrepository;

public class TokenRepository : ITokenReposetory
{
    private readonly IConfiguration _configuration;

    public TokenRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string createjwt(IdentityUser user, List<string> Roles)
    {
        var Climes = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email),
        };
        Climes.AddRange(Roles.Select(r => new Claim(ClaimTypes.Role, r)));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
        var cerdntial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["jwt:Issure"],
            audience: _configuration["jwt:Audience"],
            claims: Climes,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: cerdntial
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}