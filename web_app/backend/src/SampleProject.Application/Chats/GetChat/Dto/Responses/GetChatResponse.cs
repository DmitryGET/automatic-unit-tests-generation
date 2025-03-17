using System;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Chats.GetChat.Dto.Responses;

public record struct GetChatResponse
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("createdAt")]
    public required DateTime CreatedAt { get; init; }
}