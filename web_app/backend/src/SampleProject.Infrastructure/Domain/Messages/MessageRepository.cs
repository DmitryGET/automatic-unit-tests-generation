using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Messages;
using SampleProject.Infrastructure.Database;

namespace SampleProject.Infrastructure.Domain.Messages;

public class MessageRepository : IMessageRepository
{
    private readonly ApplicationContext _context;

    public MessageRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<Message> GetByIdAsync(Guid messageid)
    {
        return await _context.Messages.FindAsync(messageid);
    }

    public async Task<List<Message>> GetMessagesByChatIdAsync(Guid chatId)
    {
        return await _context.Messages
            .Where(x => x.ChatId == chatId)
            .ToListAsync();
    }

    public async Task DeleteAsync(Guid messageId)
    {
        var existingMessage = await _context.Messages.FindAsync(messageId);
        if (existingMessage is not null)
        {
            _context.Messages.Remove(existingMessage);
            await _context.SaveChangesAsync();
        }
    }
}