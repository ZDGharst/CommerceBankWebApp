using Microsoft.EntityFrameworkCore;
using DatabaseAdministration.Models;

namespace DatabaseAdministration.Data
{
    public class DbaContext : DbContext
    {
        public DbaContext (DbContextOptions<DbaContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Financial_Transaction> Financial_Transaction { get; set; }
        public DbSet<Notification> Notification { get; set; }
    }
}
