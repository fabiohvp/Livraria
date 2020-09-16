using Domain.Configurations;
using Domain.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Repository
{
    public partial class LivrariaContext : DbContext
    {
        public LivrariaContext()
            : base("name=LivrariaContext")
        {
        }

        public virtual DbSet<Aluguel> Aluguel { get; set; }
        public virtual DbSet<Livro> Livro { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Conventions
                .Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new AluguelConfiguration());
            modelBuilder.Configurations.Add(new LivroConfiguration());
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
        }
    }
}
