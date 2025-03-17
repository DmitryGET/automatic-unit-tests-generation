using System;
using System.Text.Json.Serialization;
using SampleProject.Domain.Chats;
using SampleProject.Domain.Users;

namespace SampleProject.Domain.Messages;

public class Message
{
    public Guid Id { get; set; }

    public Guid ChatId { get; set; }

    public int Order { get; set; }

    public string Role { get; set; }

    public string Content { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Chat Chat { get; set; }
}