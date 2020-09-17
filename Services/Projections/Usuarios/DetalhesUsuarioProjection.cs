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
            Email = o.Email,
            Id = o.Id,
            IdUsuarioCadastrador = o.IdUsuarioCadastrador,
            Nome = o.Nome,
            Permissao = o.Permissao
        };

        public class Projection
        {
            public string Email { get; set; }
            public string Id { get; set; }
            public string IdUsuarioCadastrador { get; set; }
            public string Nome { get; set; }
            public Permissao Permissao { get; set; }
        }
    }
}
