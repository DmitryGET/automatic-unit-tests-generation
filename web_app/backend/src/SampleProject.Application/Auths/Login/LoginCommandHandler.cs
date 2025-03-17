using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Auths.Login.Dto.Responses;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Tokens;

namespace SampleProject.Application.Auths.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginCommandHandler(
        UserManager<IdentityUser> userManager, 
        SignInManager<IdentityUser> signInManager, 
        IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUserByUsernameOrThrowAsync(request.Username);

        await ValidateUserPasswordAsync(user, request.Password);
        
        return new LoginResponse
        {
            UserId = user.Id,
            AccessToken = _jwtTokenService.CreateToken(user)
        };
    }

    private async Task<IdentityUser> GetUserByUsernameOrThrowAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
    
        if (user is null)
        {
            throw new EntityNotFoundException($"User with username '{username}' was not found");
        }

        return user;
    }

    private async Task ValidateUserPasswordAsync(IdentityUser user, string password)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            throw new InvalidPasswordException("Invalid password or login during authorization");
        }
    }
}