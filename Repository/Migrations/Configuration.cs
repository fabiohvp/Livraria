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
        //isso é apenas para testes, não vai dar tempo de fazer melhor
        public const string EmailAdministrador = "admin@livraria.com";
        public static string IdUsuarioCadastrador = Guid.NewGuid().ToString();

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
            var usuario = new Usuario
            {
                IdUsuarioCadastrador = IdUsuarioCadastrador,
                Email = EmailAdministrador,
                Nome = "Admin",
                Permissao = Permissao.Administrador,
                Senha = "123456".Encriptar()
            };

            if (!repository.Recuperar<Usuario>().Any(o => o.Email == usuario.Email))
            {
                repository.Inserir(usuario);
                repository.Salvar();
                IdUsuarioCadastrador = usuario.Id;
            }
        }
    }
}
