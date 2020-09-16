using Domain.Models;
using Repository;

namespace Services.Workflows.Livros
{
    public class RemoverWorkflow : Workflow<Livro>
    {
        public RemoverWorkflow(IRepository repository)
            : base(repository)
        {
        }
        protected override Livro ExecuteWorkflow(Livro candidate)
        {
            Repository.Remover(candidate);
            Repository.Salvar();
            return candidate;
        }
    }
}
