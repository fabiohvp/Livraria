using Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace Domain.Configurations
{
    public class UsuarioConfiguration : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguration()
        {
            Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(Entidade.IdLength);

            Property(e => e.IdUsuarioCadastrador)
                .IsRequired()
                .HasMaxLength(Entidade.IdLength)
                .IsUnicode(true);

            Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            Property(e => e.Senha)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(true);

            HasMany(e => e.Alugueis)
                .WithRequired(e => e.Usuario)
                .WillCascadeOnDelete(false);
        }
    }
}