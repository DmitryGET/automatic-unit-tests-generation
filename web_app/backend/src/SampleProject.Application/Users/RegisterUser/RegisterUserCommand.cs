using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Users.RegisterUser.Dto.Responses;

namespace SampleProject.Application.Users.RegisterUser;

public class RegisterUserCommand : CommandBase<RegisterUserResponse>
{
    public RegisterUserCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    public string Username { get; set; }

    public string Password { get; set; }
}