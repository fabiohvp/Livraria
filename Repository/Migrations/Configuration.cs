namespace Repository.Migrations
{
    using Domain;
    using Domain.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<LivrariaContext>
    {
        public Usuario Usuario = new Usuario
        {
            IdUsuarioCadastrador = Guid.NewGuid().ToString(),
            Email = "admin@livraria.com",
            Nome = "Admin",
            Permissao = Permissao.Administrador,
            Senha = "123456".Encriptar()
        };

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LivrariaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            RunSeed(context);
        }

        public void RunSeed(DbContext context)
        {
            var repository = new LivrariaRepository(context);

            if (!repository.Recuperar<Usuario>().Any(o => o.Email == Usuario.Email))
            {
                repository.Inserir(Usuario);
                repository.Salvar();
            }
        }
    }
}
