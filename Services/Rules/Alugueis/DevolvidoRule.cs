using Domain.Models;
using System;
using System.Linq.Expressions;

namespace Services.Rules.Alugueis
{
    public class DevolvidoRule : StateRule<Aluguel>
    {
        public override Expression<Func<Aluguel, bool>> Predicate => aluguel => aluguel.DataDevolucao.HasValue;
    }
}
