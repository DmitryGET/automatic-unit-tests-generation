using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Chats.GetChat.Dto.Responses;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Tokens;
using SampleProject.Domain.Chats;

namespace SampleProject.Application.Chats.GetChat;

public class GetChatCommandHandler : IRequestHandler<GetChatCommand, GetChatResponse>
{
    private readonly IChatRepository _chatRepository;
    private readonly IJwtTokenService _jwtTokenService;
    

    public GetChatCommandHandler(IChatRepository chatRepository, IJwtTokenService jwtTokenService)
    {
        _chatRepository = chatRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<GetChatResponse> Handle(GetChatCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _jwtTokenService.GetCurrentUserIdFromJwtToken();
        
        var chat = await _chatRepository.GetChatByChatIdAndUserId(request.ChatId, currentUserId);

        if (chat is null)
        {
            throw new EntityNotFoundException($"Chat with id '{request.ChatId}' was not found for user with id '{currentUserId}'");
        }

        return new GetChatResponse
        {
            Name = chat.Name,
            CreatedAt = chat.CreatedAt
        };

    }
}