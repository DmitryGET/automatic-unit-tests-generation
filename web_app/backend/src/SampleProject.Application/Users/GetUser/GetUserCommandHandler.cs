using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Users.GetUser.Dto.Responses;
using SampleProject.Domain.Users;

namespace SampleProject.Application.Users.GetUser;

public class GetUserCommandHandler : IRequestHandler<GetUserCommand, GetUserResponse>
{
    private readonly UserManager<IdentityUser> _userManager;

    public GetUserCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GetUserResponse> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user == null)
        {
            throw new EntityNotFoundException($"User with id '{request.Id}' was not found");
        }

        return new GetUserResponse
        {
            Username = user.UserName,
            Email = user.Email
        };
    }
}