using Domain.Models;
using Repository;

namespace Services.Workflows.Usuarios
{
    public class RemoverWorkflow : Workflow<Usuario>
    {
        public RemoverWorkflow(IRepository repository)
            : base(repository)
        {
        }
        protected override Usuario ExecuteWorkflow(Usuario candidate)
        {
            Repository.Remover(candidate);
            Repository.Salvar();
            return candidate;
        }
    }
}
