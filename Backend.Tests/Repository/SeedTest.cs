using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.Migrations;
using System.Linq;

namespace Backend.Tests.Repository
{
    [TestClass]
    public class SeedTest
    {
        [TestMethod]
        public void RunSeed()
        {
            var context = TestRepository.CreateContext();
            var configuration = new Configuration();
            configuration.RunSeed(context);

            var repository = new LivrariaRepository(context);
            var usuario = repository
                .RecuperarNoTracking<Usuario>()
                .Single(o => o.Email == configuration.Usuario.Email);

            Assert.IsNotNull(usuario);
        }
    }
}
