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
                .FirstOrDefault(o => o.Id == candidate.Id && !o.DataDevolucao.HasValue);

            if (original == default)
            {
                throw new InvalidOperationException("Não há locação em aberto para este livro");
            }

            var totalPrevisto = original.ValorPrevisto;
            var totalPago = candidate.ValorPago + original.ValorPago;
            var diasEmAtraso = original.DataLocacao.Date.AddDays(original.QuantidadeDias).Subtract(DateTime.Today).TotalDays;

            if (diasEmAtraso > 0)
            {
                totalPrevisto += ((decimal)diasEmAtraso * original.Livro.ValorAluguel);
            }

            if (totalPago < totalPrevisto)
            {
                throw new InvalidOperationException($"O total pago foi de {totalPago}, porém a dívida é de {original.ValorPrevisto}, houve atraso de {diasEmAtraso} dias");
            }

            original.DataDevolucao = DateTime.Now;
            original.ValorPago = totalPago;

            Repository.Inserir(candidate);
            Repository.Salvar();
            return candidate;
        }
    }
}
