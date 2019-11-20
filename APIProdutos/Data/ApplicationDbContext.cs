using Microsoft.EntityFrameworkCore;
using APILivraria.Models;

namespace APILivraria.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :
            base(options) { }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Livro>()
                .HasKey(p => p.CodigoBarras);
            modelBuilder.Entity<Usuario>()
                .HasKey(p => p.ID);
        }
    }
}