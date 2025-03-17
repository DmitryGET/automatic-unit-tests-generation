using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Chats;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Domain.Chats;

public class ChatRepository : IChatRepository
{
    private readonly ApplicationContext _context;

    public ChatRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Chat chat)
    {
        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();
    }

    public async Task<Chat> GetByIdAsync(Guid chatId)
    {
        return await _context.Chats.FindAsync(chatId);
    }

    public async Task<Chat> GetChatByChatIdAndUserId(Guid chatId, Guid userId)
    {
        return await _context.Chats.FirstOrDefaultAsync(x => x.ChatId == chatId && x.CreatorUserId == userId);
    }

    public async Task<List<Chat>> GetChatsByUserId(Guid userId)
    {
        return await _context.Chats
            .OrderBy(x => x.CreatedAt)
            .Where(x => x.CreatorUserId == userId)
            .ToListAsync();
    }
    
    public async Task UpdateAsync(Chat chat)
    {
        var existingChat = await _context.Chats.FindAsync(chat.ChatId);
        if (existingChat is not null)
        {
            existingChat.Name = chat.Name;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(Guid chatId)
    {
        var existingChat = await _context.Chats.FindAsync(chatId);
        if (existingChat is not null)
        {
            _context.Chats.Remove(existingChat);
            await _context.SaveChangesAsync();
        }
    }
}