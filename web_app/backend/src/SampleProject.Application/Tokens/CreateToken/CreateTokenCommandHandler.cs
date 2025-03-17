using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Tokens.CreateToken.Dto.Responses;
using SampleProject.Domain.Users;
using SampleProject.Infrastructure.Auth;

namespace SampleProject.Application.Tokens.CreateToken;

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, CreateTokenResponse>
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUserRepository _userRepository;
    
    public CreateTokenCommandHandler(IPasswordHasher<User> passwordHasher, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<CreateTokenResponse> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsername(request.Username);

        if (user is null)
        {
            throw new EntityNotFoundException($"User with username {request.Username} not found");
        }

        if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) 
            == PasswordVerificationResult.Failed)
        {
            throw new InvalidPasswordException($"Invalid password for user {request.Username}");
        }

        var claims = new List<Claim>()
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Username),
            new(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
        };
        
        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

        var tokenCreationDateTime = DateTime.UtcNow;
        var tokenExpirationDateTime = tokenCreationDateTime.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime));
        
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            notBefore: tokenCreationDateTime,
            claims: claimsIdentity.Claims,
            expires: tokenExpirationDateTime,
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new CreateTokenResponse
        {
            AccessToken = encodedJwt
        };
    }
}