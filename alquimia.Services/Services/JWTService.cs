//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using backendAlquimia.alquimia.Data.Entities;
using backendAlquimia.alquimia.Services.Interfaces;

namespace backendAlquimia.alquimia.Services
{
    public class JwtService : IJwtService
    {
        //private readonly IConfiguration _config;

        //public JwtService(IConfiguration config)
        //{
        //    _config = config;
        //}

    //    public string GenerateToken(Usuario user, IList<string> roles)
    //    {
    //        var claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //        new Claim(ClaimTypes.Name, user.UserName),
    //        new Claim(ClaimTypes.Email, user.Email)
    //    };

    //        foreach (var role in roles)
    //        {
    //            claims.Add(new Claim(ClaimTypes.Role, role));
    //        }

    //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
    //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //        var token = new JwtSecurityToken(
    //            issuer: _config["Jwt:Issuer"],
    //            audience: _config["Jwt:Audience"],
    //            claims: claims,
    //            expires: DateTime.UtcNow.AddHours(1),
    //            signingCredentials: creds
    //        );

    //        return new JwtSecurityTokenHandler().WriteToken(token);
    //    }
    }
}
