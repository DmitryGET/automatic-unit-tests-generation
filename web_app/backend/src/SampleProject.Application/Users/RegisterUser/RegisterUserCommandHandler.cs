using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Configuration.Rule;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Tokens;
using SampleProject.Application.Users.RegisterUser.Dto.Responses;
using SampleProject.Domain.SharedKernel;
using SampleProject.Domain.Users;

namespace SampleProject.Application.Users.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IRuleChecker _ruleChecker;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterUserCommandHandler(
        IRuleChecker ruleChecker, 
        UserManager<IdentityUser> userManager, 
        IJwtTokenService jwtTokenService)
    {
        _ruleChecker = ruleChecker;
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        _ruleChecker.CheckRule(new PasswordMustBeComplexRule(request.Password));

        await ThrowIfUsernameNonUnique(request.Password);
        
        var user = await RegisterUserAsync(request.Username, request.Password);
        await _userManager.AddClaimAsync(user, new Claim(nameof(user.Id), user.Id));
        
        return new RegisterUserResponse
        {
            UserId = user.Id,
            AccessToken = _jwtTokenService.CreateToken(user)
        };
    }

    private async Task ThrowIfUsernameNonUnique(string username)
    {
        var foundUserByUsername = await _userManager.FindByNameAsync(username);

        if (foundUserByUsername is not null)
        {
            throw new NonUniqueUsernameException($"User with username {username} already exist");
        }
    }

    private async Task<IdentityUser> RegisterUserAsync(string username, string password)
    {
        var user = new IdentityUser { UserName = username };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User registration failed: {errors}");
        }

        return user;
    }
}