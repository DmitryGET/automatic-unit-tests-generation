using System.Text.Json.Serialization;
using SampleProject.Domain.Users;

namespace SampleProject.Application.Messages.Dto;

public record struct MessageInfo
{
    [JsonPropertyName("role")]
    public string Role { get; init; } 

    [JsonPropertyName("content")]
    public string Content { get; init; }
}