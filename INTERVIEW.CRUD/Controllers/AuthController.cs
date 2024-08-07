using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using INTERVIEW.CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace INTERVIEW.CRUD.Controllers;
public class AuthController : ControllerBase
{
    private JwtSetting _jwtSetting;

    public AuthController(IOptions<JwtSetting> jwtSetting)
    {
        _jwtSetting = jwtSetting.Value;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody]LoginRequest user)
    {
        var username = user.UserName;
        var password = user.Password;
        if (username == "test" && password == "password")
        {
            var access_token = GenerateJwtToken(username);
            return Ok(new { access_token });
        }

        return Unauthorized();
    }

    private string GenerateJwtToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,"Admin"),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,"DBA")
            };

        var token = new JwtSecurityToken(
            issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            claims: claims,
            expires: DateTime.Now.Add(_jwtSetting.TokenExpiresAfter),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

