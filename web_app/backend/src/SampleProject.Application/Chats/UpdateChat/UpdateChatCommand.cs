using System;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Chats.UpdateChat;

public class UpdateChatCommand : CommandBase
{
    public UpdateChatCommand(Guid chatId, string name)
    {
        ChatId = chatId;
        Name = name;
    }
    
    public Guid ChatId { get; set; }

    public string Name { get; set; }
}