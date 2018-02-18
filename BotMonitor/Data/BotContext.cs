using BotMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace BotMonitor.Data
{
    public class BotContext : DbContext
    {
        public DbSet<Bot> Bots { get; set; }

        public DbSet<Instruction> Instructions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\SQLite\\BloogBot.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instruction>()
                .HasIndex(b => b.BotId)
                .IsUnique();
        }
    }
}
