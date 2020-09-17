using Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace Domain.Configurations
{
    public class LivroConfiguration : EntityTypeConfiguration<Livro>
    {
        public LivroConfiguration()
        {
            Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(Entidade.IdLength);

            Property(e => e.IdUsuarioCadastrador)
                .IsRequired()
                .HasMaxLength(Entidade.IdLength)
                .IsUnicode(true);

            Property(e => e.Autor)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            HasMany(e => e.Alugueis)
                .WithRequired(e => e.Livro)
                .WillCascadeOnDelete(false);
        }
    }
}