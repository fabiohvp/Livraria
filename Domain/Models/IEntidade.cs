namespace Domain.Models
{
    public interface IEntidade
    {
        string Id { get; set; }
    }

    public abstract class Entidade
    {
        public const int IdLength = 36;
    }
}