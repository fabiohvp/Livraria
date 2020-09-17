namespace Repository.Migrations
{
    using Domain;
    using Domain.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;

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

                //para agilizar vou assumir que se não tem usuário também não tem o resto
                for(var i = 0; i < 5; i++)
                {
                    repository.Inserir(GerarLivro());
                }
                repository.Salvar();
            }
        }

        private Livro GerarLivro()
        {
            return new Livro
            {
                Ano = (short)RandomNumber(1990, DateTime.Today.Year),
                Autor = RandomString(RandomNumber(11, 35)),
                IdUsuarioCadastrador = IdUsuarioCadastrador,
                Nome = RandomString(RandomNumber(22, 50)),
                ValorAluguel = RandomNumber(150, 1000) / 100,
                Volume = (short)RandomNumber(1, 5)
            };
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }


        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

    }
}
