using Domain.Models;
using Repository;
using System.Linq;

namespace Services.Workflows.Usuarios
{
    public class InserirWorkflow : Workflow<Usuario>
    {
        public InserirWorkflow(IRepository repository)
            : base(repository)
        {
        }

        protected override Usuario ExecuteWorkflow(Usuario candidate)
        {
            var jaExiste = Repository
                .Recuperar<Usuario>()
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
