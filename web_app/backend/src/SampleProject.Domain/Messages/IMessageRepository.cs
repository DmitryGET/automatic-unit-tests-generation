using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleProject.Domain.Messages;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    
    Task<Message> GetByIdAsync(Guid messageId);
    
    Task<List<Message>> GetMessagesByChatIdAsync(Guid chatId);
    
    Task DeleteAsync(Guid messageId);
}