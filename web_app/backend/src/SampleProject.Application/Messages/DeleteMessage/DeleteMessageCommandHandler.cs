using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Exceptions;
using SampleProject.Application.Messages.Constants;
using SampleProject.Application.Tokens;
using SampleProject.Domain.Chats;
using SampleProject.Domain.Messages;

namespace SampleProject.Application.Messages.DeleteMessage;

public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, Unit>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public DeleteMessageCommandHandler(
        IMessageRepository messageRepository,
        IChatRepository chatRepository,
        IJwtTokenService jwtTokenService)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetByIdAsync(request.MessageId);

        if (message is null)
        {
            throw new EntityNotFoundException($"Message with id '{request.MessageId}' not found");
        }
        
        var currentUserId = _jwtTokenService.GetCurrentUserIdFromJwtToken();
        
        var existingChat = await _chatRepository.GetChatByChatIdAndUserId(message.ChatId, currentUserId);
        
        if (existingChat is null)
        {
            throw new ForbiddenException($"User with id '{currentUserId}' is not a participant of the chat " +
                                         $"with id '{message.ChatId}' and cannot remove message with id '{request.MessageId}'");
        }

        if (message.Role == LlamaMessageRole.Assistant)
        {
            throw new ForbiddenException($"Forbidden to delete the message with role '{LlamaMessageRole.Assistant}'");
        }

        await _messageRepository.DeleteAsync(request.MessageId);
        
        return Unit.Value;
    }
}