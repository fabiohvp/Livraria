using Domain.Models;
using Repository;
using Services.Rules;
using System.Security.Authentication;

namespace Services.Workflows.Usuarios
{
    public class InserirWorkflow : Workflow<Usuario>
    {
        public readonly string IdUsuarioCadastrador;

        public InserirWorkflow(IRepository repository, string idUsuarioCadastrador)
            : base(repository)
        {
            IdUsuarioCadastrador = idUsuarioCadastrador;
        }

        protected override Usuario ExecuteWorkflow(Usuario candidate)
        {
            var emailRule = new EmailRule();

            if (!emailRule.IsSatisfied(candidate.Email))
            {
                throw new InvalidCredentialException("E-mail inválido");
            }

            candidate.IdUsuarioCadastrador = IdUsuarioCadastrador;
            Repository.Inserir(candidate);
            Repository.Salvar();
            return candidate;
        }
    }
}
