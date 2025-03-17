using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Auths.Login.Dto.Requests;

public record struct LoginRequest
{
    [JsonPropertyName("username")]
    [Required]
    public required string Username { get; set; }

    [JsonPropertyName("password")]
    [Required]
    public required string Password { get; set; }
}