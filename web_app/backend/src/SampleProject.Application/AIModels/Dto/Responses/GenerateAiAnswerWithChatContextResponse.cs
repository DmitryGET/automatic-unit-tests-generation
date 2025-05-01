using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SampleProject.Application.AIModels.Dto.Responses;

public record GenerateAiAnswerWithChatContextResponse
{
    [JsonPropertyName("model")]
    public string Model { get; init; }
    
    [JsonPropertyName("created")]
    public long CreatedAt { get; init; }

    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; }
}

public class Choice
{
    [JsonPropertyName("index")]
    public int Index { get; set; }
    
    [JsonPropertyName("message")]
    public Message Message { get; set; }
}

public class Message
{
    [JsonPropertyName("role")]
    public string Role { get; set; }
    
    [JsonPropertyName("content")]
    public string Content { get; set; }
}