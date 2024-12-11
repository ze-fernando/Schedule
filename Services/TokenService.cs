using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Schedule.Services;

public class TokenService
{
    public static string Generate(string email)
    {
        var date = DateTime.Now;
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Settings.Secret);
        var tokenDesc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]{
                new Claim(ClaimTypes.Name, email)
            }),
            NotBefore = date,
            Expires = date.AddHours(1),
            SigningCredentials = new SigningCredentials(
                  new SymmetricSecurityKey(key),
                  SecurityAlgorithms.HmacSha256Signature
              )
        };
        var token = tokenHandler.CreateToken(tokenDesc);
        return tokenHandler.WriteToken(token);
    }
}
