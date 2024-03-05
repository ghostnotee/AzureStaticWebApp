using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Authentication;
using Domain.Users;
using Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class JwtTokenGenerator(AppSettings appSettings) : IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret!)), SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName + ' ' + (user.MiddleName == null ? string.Empty : user.MiddleName + ' ') + user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            appSettings.Issuer,
            appSettings.Audience,
            expires: DateTime.UtcNow.AddMinutes(appSettings.ExpireMinutes),
            claims: claims,
            signingCredentials: signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}