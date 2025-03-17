using System;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Chats.CreateChat.Dto.Responses;

public record struct CreateChatResponse
{
    [JsonPropertyName("chatId")]
    public required Guid ChatId { get; init; }
}