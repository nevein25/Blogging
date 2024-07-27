using Blogging.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blogging.Infrastructure.Authentication;
internal class JwtTokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(int id, string username, string email)
    {
        var tokenKey = _configuration["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName,username),
            new Claim(JwtRegisteredClaimNames.Email,email)

        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
