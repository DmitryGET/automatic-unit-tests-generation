using System;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Auths.Login.Dto.Responses;

public record struct LoginResponse
{
    [JsonPropertyName("userId")]
    public required string UserId { get; set; }
    
    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; set; }
}