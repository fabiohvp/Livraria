using Backend.Tests.Repository;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Services.Workflows.Usuarios;
using System.Linq;

namespace Backend.Tests.Workflows.Usuarios
{
    [TestClass]
    public class EditarTest
    {
        [TestMethod]
        public void Editar()
        {
            var repository = TestRepository.CreateRepository();
            var usuario = RecuperarUsuario(repository);
            usuario.Email = "a" + usuario.Email;

            var workflow = new EditarWorkflow(repository);
            workflow.Execute(usuario);

            //pegar o dado atual no banco de dados
            var usuarioEditado = RecuperarUsuario(repository);

            Assert.AreEqual(usuarioEditado.Email, usuario.Email);
        }

        private Usuario RecuperarUsuario(IRepository repository)
        {
            var usuario = repository
                .RecuperarNoTracking<Usuario>()
                .First();
            return usuario;
        }
    }
}
