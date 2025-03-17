using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Users.RegisterUser.Dto.Requests;

public record struct RegisterUserRequest
{
    [JsonPropertyName("username")]
    [Required]
    public required string Username { get; init; }

    [JsonPropertyName("password")]
    [Required]
    public required string Password { get; init; }
}