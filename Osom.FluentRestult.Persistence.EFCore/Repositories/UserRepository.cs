using Microsoft.EntityFrameworkCore;
using Osom.FluentRestult.Domain.Entities;
using Osom.FluentRestult.Domain.Persistence;
using Osom.FluentRestult.Persistence.EFCore.Contexts;

namespace Osom.FluentRestult.Persistence.EFCore.Repositories
{
    public class UserRepository(OsomDbContext companyNameDbContext) : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            await companyNameDbContext.Users.AddAsync(user);
            await companyNameDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            companyNameDbContext.Users.Remove(user);
            await companyNameDbContext.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await companyNameDbContext.Users.ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await companyNameDbContext.Users.FindAsync(id);
        }

        public async Task<User> GetAsync(string email)
        {
            return await companyNameDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateAsync(User user)
        {
            companyNameDbContext.Users.Update(user);
            await companyNameDbContext.SaveChangesAsync();
        }
    }
}
