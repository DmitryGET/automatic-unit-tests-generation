using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Messages.GetMessagesByChat.Dto.Requests;

public record struct GetMessagesByChatRequest
{
    [JsonPropertyName("chatId")]
    [Required]
    public required Guid ChatId { get; init; }
}