using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Users.Mappings;
using SampleProject.Application.Users.UpdateUser.Dto.Responses;
using SampleProject.Domain.Users;

namespace SampleProject.Application.Users.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IPasswordHasher<IdentityUser> _passwordHasher;

    public UpdateUserCommandHandler(IPasswordHasher<IdentityUser> passwordHasher, UserManager<IdentityUser> userManager)
    {
        _passwordHasher = passwordHasher;
        _userManager = userManager;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (existingUser is null)
        {
            throw new EntityNotFoundException($"User with id {request.UserId} not found");
        }

        existingUser.UserName = request.Username;
        existingUser.Email = request.Email;

        if (!string.IsNullOrEmpty(request.Password))
        {
            existingUser.PasswordHash = _passwordHasher.HashPassword(existingUser, request.Password);
        }

        await _userManager.UpdateAsync(existingUser);

        return existingUser.ToUpdateUserResponse();
    }
}