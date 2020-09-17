using Domain.Models;
using Repository;

namespace Services.Workflows.Alugueis
{
    public class CalcularValorPrevistoWorkflow : Workflow<Aluguel, decimal>
    {
        public readonly Livro Livro;

        public CalcularValorPrevistoWorkflow(IRepository repository, Livro livro)
            : base(repository)
        {
            Livro = livro;
        }

        protected override decimal ExecuteWorkflow(Aluguel candidate)
        {
            return Livro.ValorAluguel * candidate.QuantidadeDias;
        }
    }
}
