using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Questor.Entities;

namespace Questor.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Banco> Banco { get; set; }
        public DbSet<Boleto> Boleto { get; set; }

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


            modelBuilder.Entity<Boleto>(entity =>
            {
                entity.Property(bl => bl.NomePagador).IsRequired().HasMaxLength(200);

                entity.Property(bl => bl.NomeBeneficiario).IsRequired().HasMaxLength(200);

                entity.Property(bl => bl.CpfCnpjPagador).IsRequired().HasMaxLength(20);

                entity.Property(bl => bl.CpfCnpjBeneficiario).IsRequired().HasMaxLength(20);

                entity.Property(bl => bl.Valor).IsRequired();

                entity.Property(bl => bl.Observacao).IsRequired(false).HasMaxLength(500);

                entity.Property(bl => bl.DataVencimento).IsRequired();
                
                entity.Property(bl => bl.BancoId).IsRequired();

            });
        }

    }
}
