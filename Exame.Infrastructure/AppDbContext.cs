using Exame.Domain;
using Microsoft.EntityFrameworkCore;

namespace Exame.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        }
    }

}
