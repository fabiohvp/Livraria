using System.Collections.Generic;

namespace Domain.Models
{
    public class Usuario : IEntidade
    {
        public string Id { get; set; }
        public string IdUsuarioCadastrador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Permissao Permissao { get; set; }

        public virtual ICollection<Aluguel> Alugueis { get; set; } = new HashSet<Aluguel>();
    }
}