using Domain.Models;
using System;
using System.Linq;

namespace Repository
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> Recuperar<T>() where T : class, IEntidade;
        void Editar<T>(T obj) where T : class, IEntidade;
        void Inserir<T>(T obj) where T : class, IEntidade;
        void Remover<T>(string id) where T : class, IEntidade;
        void Remover<T>(T obj) where T : class, IEntidade;
        void Salvar();
    }
}