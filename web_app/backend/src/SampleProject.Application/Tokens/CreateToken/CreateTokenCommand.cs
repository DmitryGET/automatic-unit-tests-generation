using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Tokens.CreateToken.Dto.Responses;

namespace SampleProject.Application.Tokens.CreateToken;

public class CreateTokenCommand : CommandBase<CreateTokenResponse>
{
    public string Username { get; set; }

    public string Password { get; set; }

    public CreateTokenCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
}