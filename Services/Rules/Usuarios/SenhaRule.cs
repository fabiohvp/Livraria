using Domain.Models;
using System;
using System.Linq.Expressions;

namespace Services.Rules.Usuarios
{
    public class SenhaRule : StateRule<Usuario>
    {
        public readonly string Senha;

        public SenhaRule(string senha)
        {
            Senha = senha.Encriptar();
        }

        public override Expression<Func<Usuario, bool>> Predicate => usuario => usuario.Senha == Senha;
    }
}
