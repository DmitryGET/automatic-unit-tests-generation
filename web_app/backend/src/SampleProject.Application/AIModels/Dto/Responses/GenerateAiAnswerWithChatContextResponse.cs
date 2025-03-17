using System;
using System.Text.Json.Serialization;
using SampleProject.Application.Messages;
using SampleProject.Application.Messages.Dto;

namespace SampleProject.Application.AIModels.Dto.Responses;

public record GenerateAiAnswerWithChatContextResponse
{
    [JsonPropertyName("model")]
    public string Model { get; init; }
    
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("message")]
    public MessageInfo Message { get; init; }

    [JsonPropertyName("done")]
    public bool Done { get; init; }
    
    [JsonPropertyName("total_duration")]
    public long TotalDuration { get; init; }
}