using Backend.Tests.Repository;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using Repository.Migrations;
using Services.Workflows.Alugueis;
using Services.Workflows.Livros;
using System;
using System.Linq;
using static Services.Workflows.Alugueis.DevolverWorkflow;

namespace Backend.Tests.Workflows.Alugueis
{
    [TestClass]
    public class AlugarTest
    {
        private IRepository Repository;
        private Livro Livro = new Livro
        {
            Ano = 2020,
            Autor = "Fábio Henriques Viana Pinto",
            Nome = "[Entrevista] SoftDesign",
            ValorAluguel = 100.30M,
            Volume = 1
        };

        [TestInitialize]
        public void Init()
        {
            Repository = TestRepository.CreateRepository(); //guardar na classe para aproveitar o contexto
            var workflow = new InserirWorkflow(Repository, Configuration.IdUsuarioCadastrador);
            workflow.Execute(Livro);
        }

        private Aluguel Alugar()
        {
            var aluguel = RecuperarAluguel();
            var workflow = new AlugarWorkflow(Repository);
            workflow.Execute(aluguel);

            Assert.IsNotNull(aluguel.DataLocacao);
            return aluguel;
        }

        [TestMethod]
        public void DevolverEmDia()
        {
            Alugar();
            var aluguel = RecuperarAluguel();
            var workflow = new DevolverWorkflow(Repository);
            aluguel = workflow.Execute(aluguel);

            Assert.IsNotNull(aluguel.DataDevolucao);
        }

        [TestMethod]
        public void DevolverComAtraso()
        {
            var aluguel = Alugar();
            aluguel.DataLocacao = aluguel.DataLocacao.AddDays(-3);//vai ficar devendo 1 dia
            Repository.Editar(aluguel);
            Repository.Salvar();

            var devolucao = RecuperarAluguel();
            devolucao.ValorPago = 15.00M;

            Assert.ThrowsException<PagamentoInsuficienteException>(() =>
            {
                var workflow = new DevolverWorkflow(Repository);
                workflow.Execute(devolucao);
            });
        }

        private Aluguel RecuperarAluguel()
        {
            var aluguel = new Aluguel
            {
                IdLivro = Livro.Id,
                IdUsuario = Configuration.IdUsuarioCadastrador,
                QuantidadeDias = 2,
                ValorPago = 2 * Livro.ValorAluguel //pagou tudo na hora da locação
            };
            return aluguel;
        }
    }
}
