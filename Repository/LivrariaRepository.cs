using Domain.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Repository
{
    public class LivrariaRepository : IRepository
    {
        private DbContext Context;

        public LivrariaRepository(DbContext context)
        {
            Context = context;
        }

        public IQueryable<T> Recuperar<T>()
            where T : class, IEntidade
        {
            return Context.Set<T>().AsQueryable();
        }

        public void Inserir<T>(T obj)
            where T : class, IEntidade
        {
            obj.Id = Guid.NewGuid().ToString();
            Context.Set<T>().Add(obj);
        }

        public void Editar<T>(T obj)
            where T : class, IEntidade
        {
            Context.Set<T>().AddOrUpdate(obj);
        }

        public void Remover<T>(T obj)
            where T : class, IEntidade
        {
            Context.Entry(obj).State = EntityState.Deleted;
        }

        public void Remover<T>(string id)
            where T : class, IEntidade
        {
            var obj = Context.Set<T>().Find(id);
            Remover(obj);
        }

        public void Salvar()
        {
            Context.SaveChanges();
        }

        public async void SalvarAync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
