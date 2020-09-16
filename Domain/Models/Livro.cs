using System.Collections.Generic;

namespace Domain.Models
{
    public class Livro : IEntidade
    {
        public string Id { get; set; }
        public string IdUsuarioCadastrador { get; set; }
        public string Autor { get; set; }
        public string Nome { get; set; }
        public short Ano { get; set; }
        public short Volume { get; set; }

        public virtual ICollection<Aluguel> Alugueis { get; set; } = new HashSet<Aluguel>();
    }
}