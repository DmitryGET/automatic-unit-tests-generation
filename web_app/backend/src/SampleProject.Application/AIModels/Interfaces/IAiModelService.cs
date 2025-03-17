using System.Threading.Tasks;
using SampleProject.Application.AIModels.Dto.Requests;
using SampleProject.Application.AIModels.Dto.Responses;

namespace SampleProject.Application.AIModels.Interfaces;

public interface IAiModelService
{
    public Task<GenerateAiAnswerWithChatContextResponse> GenerateAiAnswerWithChatContextAsync(
        GenerateAiAnswerWithChatContextRequest request);
}