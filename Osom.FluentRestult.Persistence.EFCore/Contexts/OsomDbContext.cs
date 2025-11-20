using Osom.FluentRestult.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Osom.FluentRestult.Persistence.EFCore.Contexts
{
    public class OsomDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public OsomDbContext(DbContextOptions<OsomDbContext> options)
            : base(options) { }
    }
}
