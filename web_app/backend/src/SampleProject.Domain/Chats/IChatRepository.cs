using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleProject.Domain.Chats;

public interface IChatRepository
{
    Task AddAsync(Chat chat);

    Task<Chat> GetChatByChatIdAndUserId(Guid chatId, Guid userId);

    Task<List<Chat>> GetChatsByUserId(Guid userId);
    
    Task UpdateAsync(Chat chat);
    
    Task DeleteAsync(Guid chatId);
}