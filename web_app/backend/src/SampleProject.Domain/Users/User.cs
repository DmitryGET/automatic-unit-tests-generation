using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Principal;
using SampleProject.Domain.RefreshTokens;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Users
{
    public class User 
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}