using GoodGive.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodGive.DAL.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Donation>()
            .HasOne(d => d.User)
            .WithMany(u => u.Donations)
            .HasForeignKey(d => d.UserId);

        modelBuilder.Entity<Donation>()
            .HasOne(d => d.Charity)
            .WithMany(c => c.Donations)
            .HasForeignKey(d => d.CharityId);

        modelBuilder.Entity<DonationGoal>()
            .HasOne(dg => dg.Charity)
            .WithMany(c => c.DonationGoals)
            .HasForeignKey(dg => dg.CharityId);

        modelBuilder.Entity<Donation>()
            .Property(d => d.Amount)
            .HasConversion<int>();

        modelBuilder.Entity<DonationGoal>()
            .Property(d => d.GoalAmount)
            .HasConversion<int>();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<DonationGoal> DonationGoals { get; set; }
    public DbSet<Charity> Charities { get; set; }
}
