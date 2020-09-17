using Backend.Models.Livros;
using Domain;
using Domain.Models;
using Repository;
using Services.Projections.Livros;
using Services.Workflows.Livros;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.Controllers
{
    public class LivroController : BaseController
    {
        public LivroController()
            : base(new LivrariaRepository(new LivrariaContext())) //O ideal seria usar Injeção de dependência mas não vou ter tempo de configurar
        {
        }

        // GET api/<controller>
        [Authorize]
        public async Task<IEnumerable<object>> Get(int page = 0, int pageSize = 10)
        {
            var detalhesLivroProjection = new DetalhesLivroProjection()
                .Predicate;

            var linhas = await Repository
                .RecuperarNoTracking<Livro>()
                .Select(detalhesLivroProjection)
                .OrderBy(o => o.Nome)
                .ThenBy(o => o.Ano)
                .Skip(page)
                .Take(pageSize)
                .ToListAsync();

            return linhas;
        }

        // GET api/<controller>/5
        [Authorize]
        public async Task<object> Get(string id)
        {
            var detalhesLivroProjection = new DetalhesLivroProjection()
                .Predicate;

            var linha = await Repository
                .RecuperarNoTracking<Livro>()
                .Select(detalhesLivroProjection)
                .FirstOrDefaultAsync(o => o.Id == id);

            return linha;
        }

        // POST api/<controller>
        [Authorize(Roles = nameof(Permissao.Administrador))]
        public void Post(InserirViewModel model)
        {
            var candidate = new Livro
            {
                Id = model.Id,
                Ano = model.Ano,
                Autor = model.Autor,
                Nome = model.Nome,
                Volume = model.Volume
            };

            var workflow = new InserirWorkflow(Repository, IdUsuario);
            workflow.Execute(candidate);
        }

        // PUT api/<controller>
        [Authorize(Roles = nameof(Permissao.Administrador))]
        public void Put(InserirViewModel model)
        {
            var candidate = new Livro
            {
                Id = model.Id,
                Ano = model.Ano,
                Autor = model.Autor,
                Nome = model.Nome,
                Volume = model.Volume
            };

            var workflow = new EditarWorkflow(Repository);
            workflow.Execute(candidate);
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = nameof(Permissao.Administrador))]
        public async Task Delete(string id)
        {
            var detalhesLivroProjection = new DetalhesLivroProjection()
                .Predicate;

            var candidate = await Repository
                .Recuperar<Livro>()
                .FirstOrDefaultAsync(o => o.Id == id);

            var workflow = new RemoverWorkflow(Repository);
            workflow.Execute(candidate);
        }
    }
}