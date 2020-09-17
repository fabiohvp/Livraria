using Domain.Models;
using LinqKit;
using Services.Projections.Livros;
using System;
using System.Linq.Expressions;

namespace Services.Projections.Alugueis
{
    public class DetalhesAluguelProjection : IProjection<Aluguel, DetalhesAluguelProjection.Projection>
    {
        public readonly Expression<Func<Livro, DetalhesLivroProjection.Projection>> DetalhesLivroProjection;

        public DetalhesAluguelProjection()
        {
            DetalhesLivroProjection = new DetalhesLivroProjection().Predicate;
        }

        public Expression<Func<Aluguel, Projection>> Predicate => o => new Projection
        {
            DataDevolucao = o.DataDevolucao,
            DataLocacao = o.DataLocacao,
            Id = o.Id,
            IdUsuario = o.IdUsuario,
            Livro = DetalhesLivroProjection.Invoke(o.Livro),
            QuantidadeDias = o.QuantidadeDias,
            Usuario = o.Usuario.Nome,
            ValorPago = o.ValorPago
        };

        public class Projection
        {
            public DateTime? DataDevolucao { get; set; }
            public DateTime DataLocacao { get; set; }
            public string Id { get; set; }
            public string IdUsuario { get; set; }
            public int QuantidadeDias { get; set; }
            public string Usuario { get; set; }
            public decimal ValorPago { get; set; }

            public DetalhesLivroProjection.Projection Livro { get; set; }
        }
    }
}
