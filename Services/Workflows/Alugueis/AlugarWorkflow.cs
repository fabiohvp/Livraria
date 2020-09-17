using Domain.Models;
using Repository;
using Services.Rules.Alugueis;
using System;
using System.Linq;

namespace Services.Workflows.Alugueis
{
    public class AlugarWorkflow : Workflow<Aluguel>
    {
        public AlugarWorkflow(IRepository repository)
            : base(repository)
        {
        }

        protected override Aluguel ExecuteWorkflow(Aluguel candidate)
        {
            var devolvidoRule = new DevolvidoRule().Predicate;

            //se o livro ainda não foi devolvido
            var disponivel = !Repository
                .Recuperar<Aluguel>()
                .Where(o => o.IdLivro == candidate.IdLivro)
                .Any(devolvidoRule);

            if (!disponivel)
            {
                throw new InvalidOperationException("Este livro está alugado");
            }

            var livro = Repository
                .RecuperarNoTracking<Livro>()
                .FirstOrDefault(o => o.Id == candidate.IdLivro);

            if (livro == default)
            {
                throw new InvalidOperationException("Livro não encontrado");
            }

            candidate.DataLocacao = DateTime.Now;

            var calcularValorPrevistoWorkflow = new CalcularValorPrevistoWorkflow(Repository, livro);
            candidate.ValorPrevisto = calcularValorPrevistoWorkflow.Execute(candidate);

            Repository.Inserir(candidate);
            Repository.Salvar();
            return candidate;
        }
    }
}
