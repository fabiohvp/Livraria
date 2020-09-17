using Backend.Controllers;
using Backend.Tests.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Migrations;
using Services.Projections.Usuarios;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Tests.Controllers.Usuarios
{
    [TestClass]
    public class UsuarioControllerTest
    {
        [TestMethod]
        public void Get()
        {
            var repository = TestRepository.CreateRepository();
            var controller = new UsuarioController(repository);
            var resultAsync = controller.Get() as Task<IEnumerable<DetalhesUsuarioProjection.Projection>>;
            var result = resultAsync.Result;

            Assert.AreEqual(1, result.Count());
            
            var usuario = result.First();
            Assert.AreEqual(usuario.Id, Configuration.IdUsuarioCadastrador);
        }
    }
}
