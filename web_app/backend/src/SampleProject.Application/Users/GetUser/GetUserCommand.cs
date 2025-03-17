using System;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Users.GetUser.Dto.Responses;

namespace SampleProject.Application.Users.GetUser;

public class GetUserCommand : CommandBase<GetUserResponse>
{
    public GetUserCommand(Guid userId)
    {
        UserId = userId;
    }
    
    public Guid UserId { get; set; }
}