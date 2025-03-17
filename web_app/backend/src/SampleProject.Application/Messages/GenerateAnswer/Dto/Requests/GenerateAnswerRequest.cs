using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SampleProject.Application.Messages.Dto;

namespace SampleProject.Application.Messages.GenerateAnswer.Dto.Requests;

public record struct GenerateAnswerRequest
{
    [JsonPropertyName("model")]
    [Required]
    public required string Model { get; init; }
    
    [JsonPropertyName("chatId")]
    [Required]
    public required Guid ChatId { get; init; }

    [JsonPropertyName("message")]
    [Required]
    public required string Message { get; set; }
}