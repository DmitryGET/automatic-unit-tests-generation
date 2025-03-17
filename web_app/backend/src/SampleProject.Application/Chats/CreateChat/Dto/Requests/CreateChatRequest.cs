using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Chats.CreateChat.Dto.Requests;

public record struct CreateChatRequest
{
    [JsonPropertyName("name")]
    [Required]
    [MinLength(1)]
    [MaxLength(300)]
    public required string Name { get; init; }
}