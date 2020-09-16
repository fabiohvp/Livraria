using Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Domain.Configurations
{
    public class AluguelConfiguration : EntityTypeConfiguration<Aluguel>
    {
        public AluguelConfiguration()
        {
            Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(Entidade.IdLength)
                .HasColumnAnnotation(
                    nameof(IEntidade.Id), 
                    new IndexAnnotation(new[] { new IndexAttribute(nameof(IEntidade.Id)) { IsUnique = true } })
                );

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