using Microsoft.IdentityModel.Tokens;
using ParkingLotManager.WebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ParkingLotManager.WebApi.Services;

public class TokenService
{
    public string GenerateToken(Company company)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var tokenDescriptor = new SecurityTokenDescriptor();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
