using SampleProject.Application.Auths.Login.Dto.Responses;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Auths.Login;

public class LoginCommand : CommandBase<LoginResponse>
{
    public LoginCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
    
    public string Username { get; set; }

    public string Password { get; set; }
}