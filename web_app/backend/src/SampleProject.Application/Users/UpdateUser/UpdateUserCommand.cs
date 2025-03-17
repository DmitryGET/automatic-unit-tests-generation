using System;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Users.UpdateUser.Dto.Responses;

namespace SampleProject.Application.Users.UpdateUser;

public class UpdateUserCommand : CommandBase<UpdateUserResponse>
{
    public UpdateUserCommand(Guid userId, string username, string password, string email)
    {
        UserId = userId;
        Username = username;
        Password = password;
        Email = email;
    }
    
    public Guid UserId { get; set; }

    public string Username { get; set; }
    
    public string Password { get; set; }

    public string Email { get; set; }
}