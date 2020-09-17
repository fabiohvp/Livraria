using Domain.Models;
using System;
using System.Linq.Expressions;

namespace Services.Projections.Livros
{
    public class DetalhesLivroProjection : IProjection<Livro, DetalhesLivroProjection.Projection>
    {
        public Expression<Func<Livro, Projection>> Predicate => o => new Projection
        {
            Ano = o.Ano,
            Autor = o.Autor,
            Id = o.Id,
            IdUsuarioCadastrador = o.IdUsuarioCadastrador,
            Nome = o.Nome,
            Volume = o.Volume,
            ValorAluguel = o.ValorAluguel
        };

        public class Projection
        {
            public short Ano { get; set; }
            public string Autor { get; set; }
            public string Id { get; set; }
            public string IdUsuarioCadastrador { get; set; }
            public string Nome { get; set; }
            public decimal ValorAluguel { get; set; }
            public short Volume { get; set; }
        }
    }
}
