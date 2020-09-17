using Domain.Models;
using Repository;
using Services.Rules;
using System;
using System.Linq;
using System.Security.Authentication;

namespace Services.Workflows.Usuarios
{
    public class EditarWorkflow : Workflow<Usuario>
    {
        public EditarWorkflow(IRepository repository)
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

            var original = Repository
                .Recuperar<Usuario>()
                .FirstOrDefault(o => o.Id == candidate.Id);

            if (original == default)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }

            if (original.Id == candidate.IdUsuarioCadastrador || original.Permissao == Domain.Permissao.Administrador)
            {
                original.Email = candidate.Email;
                original.Permissao = candidate.Permissao;
                original.Senha = candidate.Senha.Encriptar();

                Repository.Editar(candidate);
                Repository.Salvar();
                return candidate;
            }
            throw new UnauthorizedAccessException("Você não tem permissão para editar este usuário");
        }
    }
}
