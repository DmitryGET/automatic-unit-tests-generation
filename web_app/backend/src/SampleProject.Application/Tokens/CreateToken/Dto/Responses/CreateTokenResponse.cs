namespace SampleProject.Application.Tokens.CreateToken.Dto.Responses;

public record struct CreateTokenResponse
{
    public required string AccessToken { get; init; }
}