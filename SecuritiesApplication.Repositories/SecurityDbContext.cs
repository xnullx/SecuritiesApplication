using Microsoft.EntityFrameworkCore;
using SecuritiesApplication.Entities;

namespace SecuritiesApplication.Repositories
{
    public class SecurityDbContext : DbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options)
        : base(options)
        {            
        }

        public DbSet<Security> Securities { get; set; }

    }
}
