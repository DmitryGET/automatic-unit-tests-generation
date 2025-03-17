using System;
using SampleProject.Domain.Users;

namespace SampleProject.Domain.RefreshTokens
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
    
        public int UserId { get; set; }
    
        public string Token { get; set; }
    
        public DateTime ExpiresAt { get; set; }
    
        public DateTime CreatedAt { get; set; }
    
        public DateTime? RevokedAt { get; set; }
    }
}