using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Application.Messages.GetMessagesByChat.Dto.Responses;
using SampleProject.Domain.Messages;

namespace SampleProject.Application.Messages.GetMessagesByChat;

public class GetMessagesByChatCommandHandler : IRequestHandler<GetMessagesByChatCommand, GetMessagesByChatResponse>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesByChatCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<GetMessagesByChatResponse> Handle(GetMessagesByChatCommand request, CancellationToken cancellationToken)
    {
        var messagesByChat = await _messageRepository.GetMessagesByChatIdAsync(request.ChatId);
        
        return new GetMessagesByChatResponse
        {
            Messages = messagesByChat
        };
    }
}