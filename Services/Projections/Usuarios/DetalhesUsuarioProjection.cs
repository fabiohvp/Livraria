using Domain;
using Domain.Models;
using System;
using System.Linq.Expressions;

namespace Services.Projections.Usuarios
{
    public class DetalhesUsuarioProjection : IProjection<Usuario, DetalhesUsuarioProjection.Projection>
    {
        public Expression<Func<Usuario, Projection>> Predicate => o => new Projection
        {
            Id = o.Id,
            IdUsuarioCadastrador = o.IdUsuarioCadastrador,
            Nome = o.Nome,
            Email = o.Email,
            Permissao = o.Permissao
        };

        public class Projection
        {
            public string Id { get; set; }
            public string IdUsuarioCadastrador { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public Permissao Permissao { get; set; }
        }
    }
}
