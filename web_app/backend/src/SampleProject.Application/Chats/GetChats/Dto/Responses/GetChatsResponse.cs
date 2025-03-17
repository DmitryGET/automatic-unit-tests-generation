using System.Collections.Generic;
using System.Text.Json.Serialization;
using SampleProject.Domain.Chats;

namespace SampleProject.Application.Chats.GetChats.Dto.Responses;

public record struct GetChatsResponse
{
    [JsonPropertyName("chats")]
    public required IEnumerable<Chat> Chats { get; init; }
}