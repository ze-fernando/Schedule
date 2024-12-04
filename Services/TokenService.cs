using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;


public class TokenService
{
    public static string Generate()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5+IV)E2glD3xCH2rNTElZ_at9(TbG1N(E=pH)29*"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
                issuer: "schedule",
                audience: "schedule",
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.WriteToken(tokenDescriptor);

        return token;
    }
}
