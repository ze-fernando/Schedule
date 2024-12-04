using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlite("DataSource=sqlite.db; Cache=Shared");

}
