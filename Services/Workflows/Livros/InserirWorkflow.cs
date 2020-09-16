using Domain.Models;
using Repository;
using System.Linq;

namespace Services.Workflows.Livros
{
    public class InserirWorkflow : Workflow<Livro>
    {
        public InserirWorkflow(IRepository repository)
            : base(repository)
        {
        }

        protected override Livro ExecuteWorkflow(Livro candidate)
        {
            var jaExiste = Repository
                .Recuperar<Livro>()
                .Any(o => o.Id == candidate.Id);

            if (jaExiste)
            {
                var editarWorkflow = new EditarWorkflow(Repository);
                return editarWorkflow.Execute(candidate);
            }

            Repository.Inserir(candidate);
            Repository.Salvar();
            return candidate;
        }
    }
}
