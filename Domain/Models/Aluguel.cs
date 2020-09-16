using System;

namespace Domain.Models
{
    public class Aluguel : IEntidade
    {
        public string Id { get; set; }
        public string IdLivro { get; set; }
        public string IdUsuario { get; set; }
        public decimal ValorPago { get; set; }

        public DateTime DataLocacao { get; set; }
        public DateTime? DataDevolucao { get; set; }

        public virtual Livro Livro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}