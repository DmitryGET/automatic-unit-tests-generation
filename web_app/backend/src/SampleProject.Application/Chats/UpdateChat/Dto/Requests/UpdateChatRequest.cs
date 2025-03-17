using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Chats.UpdateChat.Dto.Requests;

public record struct UpdateChatRequest
{
    [JsonPropertyName("name")]
    [Required]
    public required string Name { get; init; }
}