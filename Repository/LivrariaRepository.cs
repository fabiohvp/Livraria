using Domain.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Text;

namespace Repository
{
    public class LivrariaRepository : IRepository
    {
        private DbContext Context;

        public LivrariaRepository(DbContext context)
        {
            Context = context;
        }

        public DbSet<T> Recuperar<T>()
            where T : class, IEntidade
        {
            return Context.Set<T>();
        }

        public DbQuery<T> RecuperarNoTracking<T>()
            where T : class, IEntidade
        {
            return Context.Set<T>().AsNoTracking();
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
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var text = new StringBuilder();

                foreach (var eve in e.EntityValidationErrors)
                {
                    text.Append(string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + Environment.NewLine, eve.Entry.Entity.GetType().Name, eve.Entry.State));

                    foreach (var ve in eve.ValidationErrors)
                    {
                        text.Append(string.Format("- Property: \"{0}\", Error: \"{1}\"" + Environment.NewLine, ve.PropertyName, ve.ErrorMessage));
                    }
                }
                throw new DbEntityValidationException(text.ToString(), e);
            }
            catch
            {
                throw;
            }
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
