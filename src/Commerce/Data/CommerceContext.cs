using Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Commerce.Data
{
  public class CommerceContext : DbContext
  {
    public CommerceContext(DbContextOptions<CommerceContext> options) : base(options)
    {
    }

    public DbSet<Financial_Transaction> Financial_Transaction { get; set; }
  }
}