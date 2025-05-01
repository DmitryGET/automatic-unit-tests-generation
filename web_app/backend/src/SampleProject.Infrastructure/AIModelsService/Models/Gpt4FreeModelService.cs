using System;
using System.Net.Http;
using System.Threading.Tasks;
using SampleProject.Application.AIModels.Dto.Requests;
using SampleProject.Application.AIModels.Dto.Responses;
using SampleProject.Application.AIModels.Interfaces;
using SampleProject.Infrastructure.Http.Interfaces;
using SampleProject.Infrastructure.Http.Models;

namespace SampleProject.Infrastructure.AIModelsService.Models;

public class Gpt4FreeModelService : IAiModelService
{
    private readonly IHttpRequestService _httpRequestService;

    public Gpt4FreeModelService(IHttpRequestService httpRequestService)
    {
        _httpRequestService = httpRequestService;
    }
    
    public async Task<GenerateAiAnswerWithChatContextResponse> GenerateAiAnswerWithChatContextAsync(
        GenerateAiAnswerWithChatContextRequest request)
    {
        var response = await _httpRequestService.SendRequestAsync<GenerateAiAnswerWithChatContextResponse>(
            new HttpRequestData
            {
                Method = HttpMethod.Post,
                Uri = new Uri("http://localhost:1301/v1/chat/completions"), // TODO перенести хост в файл конфигурации
                Body = request,
                ContentType = ContentType.ApplicationJson
            });
        
        return response.Body;
    }
}
