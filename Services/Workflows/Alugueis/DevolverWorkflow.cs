using Domain.Models;
using Repository;
using System;
using System.Linq;

namespace Services.Workflows.Alugueis
{
    public class DevolverWorkflow : Workflow<Aluguel>
    {
        public DevolverWorkflow(IRepository repository)
            : base(repository)
        {
        }

        protected override Aluguel ExecuteWorkflow(Aluguel candidate)
        {
            var original = Repository
                .Recuperar<Aluguel>()
                .FirstOrDefault(o => o.IdLivro == candidate.IdLivro && !o.DataDevolucao.HasValue);

            if (original == default)
            {
                throw new InvalidOperationException("Não há locação em aberto para este livro");
            }

            var totalPago = original.ValorPago + candidate.ValorPago; //valor pago na locação + valor pago na devolução
            candidate.QuantidadeDias = original.QuantidadeDias + (int)Math.Floor(DateTime.Today.Subtract(original.DataLocacao.Date.AddDays(original.QuantidadeDias)).TotalDays); //quantidadeDias + dias em atraso

            var calcularValorPrevistoWorkflow = new CalcularValorPrevistoWorkflow(Repository, original.Livro);
            var valorPrevisto = calcularValorPrevistoWorkflow.Execute(candidate);

            if (totalPago < valorPrevisto)
            { 
                throw new PagamentoInsuficienteException($"O total pago foi de {totalPago}, porém a dívida é de {original.ValorPrevisto}, houve atraso de {candidate.QuantidadeDias - original.QuantidadeDias} dias");
            }

            original.QuantidadeDias = candidate.QuantidadeDias;
            original.DataDevolucao = DateTime.Now;
            original.ValorPago = totalPago;

            Repository.Editar(original);
            Repository.Salvar();
            return original;
        }

        public class PagamentoInsuficienteException : Exception
        {
            public PagamentoInsuficienteException(string message)
                : base(message) { }
        }
    }
}
