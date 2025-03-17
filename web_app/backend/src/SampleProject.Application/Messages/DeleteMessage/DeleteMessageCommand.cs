using System;
using SampleProject.Application.Configuration.Commands;

namespace SampleProject.Application.Messages.DeleteMessage;

public class DeleteMessageCommand : CommandBase
{
    public DeleteMessageCommand(Guid messageId)
    {
        MessageId = messageId;
    }
    
    public Guid MessageId { get; set; }
}