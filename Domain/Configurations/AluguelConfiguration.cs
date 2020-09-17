using Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace Domain.Configurations
{
    public class AluguelConfiguration : EntityTypeConfiguration<Aluguel>
    {
        public AluguelConfiguration()
        {
            Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(Entidade.IdLength);

            Property(e => e.IdLivro)
                .IsRequired()
                .HasMaxLength(Entidade.IdLength)
                .IsUnicode(true);

            Property(e => e.IdUsuario)
                .IsRequired()
                .HasMaxLength(Entidade.IdLength)
                .IsUnicode(true);

            HasRequired(e => e.Livro)
                .WithMany(e => e.Alugueis)
                .HasForeignKey(e => e.IdLivro)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.Usuario)
                .WithMany(e => e.Alugueis)
                .HasForeignKey(e => e.IdUsuario)
                .WillCascadeOnDelete(false);

        }
    }
}