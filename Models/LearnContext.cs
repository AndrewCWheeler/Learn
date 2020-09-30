using Microsoft.EntityFrameworkCore;

namespace Learn.Models
{
  public class LearnContext : DbContext
  {
    public LearnContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
  }
}