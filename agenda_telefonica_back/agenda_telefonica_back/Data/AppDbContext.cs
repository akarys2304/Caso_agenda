using agenda_telefonica_back.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace agenda_telefonica_back.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Contato> Contatos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade Contato
            modelBuilder.Entity<Contato>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.telefone)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
