using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Chats.CreateChat.Dto.Responses;
using SampleProject.Application.Tokens;
using SampleProject.Domain.Chats;

namespace SampleProject.Application.Chats.CreateChat;

public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, CreateChatResponse>
{
    private readonly IChatRepository _chatRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public CreateChatCommandHandler(IChatRepository chatRepository, IJwtTokenService jwtTokenService)
    {
        _chatRepository = chatRepository;
        _jwtTokenService = jwtTokenService;
    }
    
    public async Task<CreateChatResponse> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var creatorUserId = _jwtTokenService.GetCurrentUserIdFromJwtToken();
        
        var chat = new Chat
        {
            ChatId = Guid.NewGuid(),
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            CreatorUserId = creatorUserId
        };

        await _chatRepository.AddAsync(chat);

        return new CreateChatResponse
        {
            ChatId = chat.ChatId,
        };
    }
}