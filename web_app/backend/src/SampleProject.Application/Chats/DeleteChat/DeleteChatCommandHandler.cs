using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Tokens;
using SampleProject.Domain.Chats;

namespace SampleProject.Application.Chats.DeleteChat;

public class DeleteChatCommandHandler : IRequestHandler<DeleteChatCommand, Unit>
{
    private readonly IChatRepository _chatRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public DeleteChatCommandHandler(IChatRepository chatRepository, IJwtTokenService jwtTokenService)
    {
        _chatRepository = chatRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Unit> Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _jwtTokenService.GetCurrentUserIdFromJwtToken();
        
        var existingChat = await _chatRepository.GetChatByChatIdAndUserId(request.ChatId, currentUserId);
        
        if (existingChat is null)
        {
            throw new EntityNotFoundException($"Chat with id '{request.ChatId}' was not found for user with id '{currentUserId}'");
        }
        
        await _chatRepository.DeleteAsync(request.ChatId);
        
        return Unit.Value;
    }
}