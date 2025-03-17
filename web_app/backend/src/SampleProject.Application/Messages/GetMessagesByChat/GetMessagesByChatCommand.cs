using System;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Messages.GetMessagesByChat.Dto.Responses;

namespace SampleProject.Application.Messages.GetMessagesByChat;

public class GetMessagesByChatCommand : CommandBase<GetMessagesByChatResponse>
{
    public GetMessagesByChatCommand(Guid chatId)
    {
        ChatId = chatId;
    }
    
    public Guid ChatId { get; set; }
}