using BotMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace BotMonitor.Data
{
    public class BotContext : DbContext
    {
        public DbSet<Bot> Bots { get; set; }

        public DbSet<Instruction> Instructions { get; set; }

        public DbSet<User> Users { get; set; }

        public BotContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instruction>()
                .HasIndex(i =>i.BotId)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Bot>()
                .HasIndex(b => new { b.Name, b.RealmName })
                .IsUnique();
        }
    }
}
