using Osom.FluentRestult.Domain.Entities;

namespace Osom.FluentRestult.Domain.Persistence
{
    public interface IUserRepository
    {
        public Task<User> GetAsync(int id);
        public Task<List<User>> GetAllAsync();
        public Task<User> GetAsync(string email);
        public Task AddAsync(User user);
        public Task UpdateAsync(User user);
    }
}
