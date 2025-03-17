using System.Text.Json.Serialization;

namespace SampleProject.Application.Messages.GenerateAnswer.Dto.Responses;

public record struct GenerateAnswerResponse
{
    [JsonPropertyName("content")]
    public required string Content { get; init; }
}