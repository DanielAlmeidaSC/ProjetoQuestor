using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Questor.Entities;

namespace Questor.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Banco> Banco { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>(); 

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.Property(b => b.Nome).IsRequired().HasMaxLength(100);

                entity.Property(b => b.CodigoBanco).IsRequired();
            });
        }

    }
}
