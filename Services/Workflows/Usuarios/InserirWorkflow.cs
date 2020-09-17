using Domain.Models;
using Repository;
using Services.Rules;
using System.Security.Authentication;

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
            var emailRule = new EmailRule();

            if (!emailRule.IsSatisfied(candidate.Email))
            {
                throw new InvalidCredentialException("E-mail inválido");
            }

            Repository.Inserir(candidate);
            Repository.Salvar();
            return candidate;
        }
    }
}
