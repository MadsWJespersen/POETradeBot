using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POETestBot.Entities
{
    public class ExileContext : DbContext, IExileContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Item> Items { get; set; }
        public ExileContext(DbContextOptions<ExileContext> options)
               : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasKey(u => u.ID);

            modelBuilder.Entity<Message>()
                        .HasKey(m => m.ID);

            modelBuilder.Entity<Item>()
                        .HasKey(i => i.ID);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Messages)
                        .WithOne(m => m.sentBy)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Items)
                        .WithOne(i => i.owner)
                        .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
