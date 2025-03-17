using System;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Users.RegisterUser.Dto.Responses;

public record struct RegisterUserResponse
{
    [JsonPropertyName("userId")]
    public required string UserId { get; init; }

    [JsonPropertyName("accessToken")]
    public required string AccessToken { get; init; }
}