using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Tokens;
using SampleProject.Domain.Chats;

namespace SampleProject.Application.Chats.UpdateChat;

public class UpdateChatCommandHandler : IRequestHandler<UpdateChatCommand>
{
    private readonly IChatRepository _chatRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public UpdateChatCommandHandler(IChatRepository chatRepository, IJwtTokenService jwtTokenService)
    {
        _chatRepository = chatRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Unit> Handle(UpdateChatCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _jwtTokenService.GetCurrentUserIdFromJwtToken();
        
        var existingChat = await _chatRepository.GetChatByChatIdAndUserId(request.ChatId, currentUserId);
        
        if (existingChat is null)
        {
            throw new EntityNotFoundException($"Chat with id '{request.ChatId}' was not found for user with id '{currentUserId}'");
        }

        var chat = new Chat
        {
            ChatId = existingChat.ChatId,
            Name = request.Name,
            CreatedAt = existingChat.CreatedAt
        };

        await _chatRepository.UpdateAsync(chat);
        
        return Unit.Value;
    }
}