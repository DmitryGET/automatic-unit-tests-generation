using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleProject.Domain.Chats;
using SampleProject.Domain.Messages;

namespace SampleProject.Infrastructure.Database;

public class ApplicationContext : IdentityDbContext<IdentityUser>
{
    public ApplicationContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Chat> Chats { get; set; }

    public DbSet<Message> Messages { get; set; }
}