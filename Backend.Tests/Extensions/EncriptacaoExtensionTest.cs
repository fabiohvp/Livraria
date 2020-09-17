using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Backend.Tests.Extensions
{
    [TestClass]
    public class EncriptacaoExtensionTest
    {
        [TestMethod]
        public void Encriptar()
        {
            var texto = "123456";
            var textoEncriptado = texto.Encriptar();
            Assert.AreEqual(texto + "a", textoEncriptado);
        }
    }
}
