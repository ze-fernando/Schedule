using Microsoft.EntityFrameworkCore;
using Schedule.Models;

namespace Schedule.Entities;

public class AppDbContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Appointment>? Schedules { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlite("DataSource=sqlite.db; Cache=Shared");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>()
            .HasOne(s => s.User)
            .WithMany(u => u.Schedules)
            .HasForeignKey(s => s.UserId); 
    }
}
