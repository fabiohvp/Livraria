using Domain.Models;
using Repository;
using System;
using System.Linq;

namespace Services.Workflows.Livros
{
    public class EditarWorkflow : Workflow<Livro>
    {
        public EditarWorkflow(IRepository repository)
            : base(repository)
        {
        }
        protected override Livro ExecuteWorkflow(Livro candidate)
        {
            var original = Repository
                .Recuperar<Livro>()
                .FirstOrDefault(o => o.Id == candidate.Id);

            if (original == default)
            {
                throw new InvalidOperationException("Livro não pode ser alterado pois não existe");
            }

            original.Nome = candidate.Nome;

            Repository.Editar(candidate);
            Repository.Salvar();
            return candidate;
        }
    }
}
