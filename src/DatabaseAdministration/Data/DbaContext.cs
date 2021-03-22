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

        public DbSet<AspNetUsers> AspNetUsers { get; set; }
        public DbSet<Customer_Info> Customer_Info { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Financial_Transaction> Financial_Transaction { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Notification_Rule> Notification_Rule { get; set; }
        public DbSet<DatabaseAdministration.Models.Customer_Account> Customer_Account { get; set; }
    }
}
