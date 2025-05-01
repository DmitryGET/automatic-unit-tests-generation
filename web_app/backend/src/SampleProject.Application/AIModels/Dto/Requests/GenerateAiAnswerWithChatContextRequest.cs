using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SampleProject.Application.Messages.Dto;

namespace SampleProject.Application.AIModels.Dto.Requests;

public class GenerateAiAnswerWithChatContextRequest
{
    [JsonPropertyName("model")]
    [Required]
    public required string Model { get; init; } // TODO перевести на enum 
    
    [JsonPropertyName("messages")]
    [Required]
    public required List<MessageInfo> Messages { get; init; } = [];
    
    [JsonPropertyName("stream")]
    [Required]
    public required bool Stream { get; init; }
}