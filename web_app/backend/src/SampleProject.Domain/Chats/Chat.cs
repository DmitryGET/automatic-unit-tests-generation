using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SampleProject.Domain.Messages;

namespace SampleProject.Domain.Chats;

public class Chat
{
    public Guid ChatId { get; set; }

    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public Guid CreatorUserId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<Message> Messages { get; set; }
}