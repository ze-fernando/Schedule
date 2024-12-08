using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }

    public DbSet<Schedule>? Schedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
      => options.UseSqlite("DataSource=sqlite.db; Cache=Shared");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(k => k.Id);
        modelBuilder.Entity<Schedule>().HasKey(k => k.Id);
    }
}
