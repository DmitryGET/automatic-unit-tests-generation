using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleProject.Domain.Users
{
    public interface IUserRepository
    {
        Task<Guid> AddAsync(User user);
    
        Task<User> GetByIdAsync(Guid userId);

        Task<User> GetByUsername(string username);
    
        Task<IEnumerable<User>> GetAllAsync();

        Task UpdateAsync(User user);

        Task RemoveAsync(Guid userId);
    }
}