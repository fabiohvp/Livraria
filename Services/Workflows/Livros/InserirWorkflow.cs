using Domain.Models;
using Repository;
using System.Linq;

namespace Services.Workflows.Livros
{
    public class InserirWorkflow : Workflow<Livro>
    {
        public readonly string IdUsuarioCadastrador;

        public InserirWorkflow(IRepository repository, string idUsuarioCadastrador)
            : base(repository)
        {
            IdUsuarioCadastrador = idUsuarioCadastrador;
        }

        protected override Livro ExecuteWorkflow(Livro candidate)
        {
            var jaExiste = Repository
                .Recuperar<Livro>()
                .Any(o => o.Id == candidate.Id);

            candidate.IdUsuarioCadastrador = IdUsuarioCadastrador;
            Repository.Inserir(candidate);
            Repository.Salvar();
            return candidate;
        }
    }
}
