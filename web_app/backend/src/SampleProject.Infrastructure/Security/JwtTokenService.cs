using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SampleProject.Application.Tokens;

namespace SampleProject.Infrastructure.Security;

public class JwtTokenService : IJwtTokenService
{
    private const int TokenExpirationSeconds = 360000;
    
    private readonly SymmetricSecurityKey _key;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTokenService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        // TODO вынести в константу
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        _httpContextAccessor = httpContextAccessor;
    }

    public string CreateToken(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddSeconds(TokenExpirationSeconds),
            SigningCredentials = credentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public Guid GetCurrentUserIdFromJwtToken()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            throw new InvalidOperationException("HttpContext is not available");
        }

        var userIdClaimValue = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userIdClaimValue is null)
        {
            throw new InvalidOperationException("User id not found in claims");
        }

        return Guid.Parse(userIdClaimValue!);
    }
}