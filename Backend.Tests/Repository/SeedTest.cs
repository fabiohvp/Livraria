using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var repository = TestRepository.CreateRepository();
            var usuario = repository
                .RecuperarNoTracking<Usuario>()
                .Single(o => o.Email == Configuration.EmailAdministrador);

            Assert.IsNotNull(usuario);
        }
    }
}
