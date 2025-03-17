using System.Collections.Generic;
using System.Text.Json.Serialization;
using SampleProject.Domain.Messages;

namespace SampleProject.Application.Messages.GetMessagesByChat.Dto.Responses;

public record struct GetMessagesByChatResponse
{
    [JsonPropertyName("messages")]
    public required IEnumerable<Message> Messages { get; init; }
}