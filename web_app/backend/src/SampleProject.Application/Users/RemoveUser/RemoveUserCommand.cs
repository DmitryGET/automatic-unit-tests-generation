using System;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Users.RemoveUser;

public class RemoveUserCommand : CommandBase
{
    public RemoveUserCommand(Guid userId)
    {
        UserId = userId;
    }
    
    public Guid UserId { get; set; }
}