using System;
using System.Net.Http;
using System.Threading.Tasks;
using SampleProject.Application.AIModels.Dto.Requests;
using SampleProject.Application.AIModels.Dto.Responses;
using SampleProject.Application.AIModels.Interfaces;
using SampleProject.Infrastructure.Http.Interfaces;
using SampleProject.Infrastructure.Http.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SampleProject.Infrastructure.AIModelsService.Models;

public class LlamaModelService : IAiModelService
{
    private readonly IHttpRequestService _httpRequestService;

    public LlamaModelService(IHttpRequestService httpRequestService)
    {
        _httpRequestService = httpRequestService;
    }
    
    public async Task<GenerateAiAnswerWithChatContextResponse> GenerateAiAnswerWithChatContextAsync(
        GenerateAiAnswerWithChatContextRequest request)
    {
        var body = JsonSerializer.Serialize(request);

        var response = await _httpRequestService.SendRequestAsync<GenerateAiAnswerWithChatContextResponse>(
            new HttpRequestData
            {
                Method = HttpMethod.Post,
                Uri = new Uri("http://llm:11434/api/chat"), // TODO перенести хост в файл конфигурации
                Body = body,
                ContentType = ContentType.TextPlain
            },
            new HttpConnectionData
            {
                ClientName = "llama",
                TimeOut = new TimeSpan(0, 10, 0)
            });
        
        return response.Body;
    }
}
