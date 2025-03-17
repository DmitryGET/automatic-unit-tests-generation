using System.ComponentModel.DataAnnotations;

namespace SampleProject.Application.Tokens.CreateToken.Dto.Requests;

public record struct CreateTokenRequest
{
    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Password { get; init; }
}