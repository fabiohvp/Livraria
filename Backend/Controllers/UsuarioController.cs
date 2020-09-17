using Backend.Models.Usuarios;
using Domain;
using Domain.Models;
using Repository;
using Services.Projections.Usuarios;
using Services.Workflows.Usuarios;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.Controllers
{
    public class UsuarioController : BaseController
    {
        public UsuarioController()
            : base(new LivrariaRepository(new LivrariaContext())) //O ideal seria usar Injeção de dependência mas não vou ter tempo de configurar
        {
        }

        public UsuarioController(IRepository repository)
            : base(repository) //Para testes unitários
        {
        }

        // GET api/<controller>
        [Authorize(Roles = nameof(Permissao.Administrador))]
        public async Task<IEnumerable<DetalhesUsuarioProjection.Projection>> Get(int page = 0, int pageSize = 10)
        {
            var detalhesUsuarioProjection = new DetalhesUsuarioProjection()
                .Predicate;

            var linhas = await Repository
                .RecuperarNoTracking<Usuario>()
                .Select(detalhesUsuarioProjection)
                .OrderBy(o => o.Nome)
                .Skip(page)
                .Take(pageSize)
                .ToListAsync();

            return linhas;
        }

        // GET api/<controller>/5
        [Authorize(Roles = nameof(Permissao.Administrador))]
        public async Task<object> Get(string id)
        {
            var detalhesUsuarioProjection = new DetalhesUsuarioProjection()
                .Predicate;

            var linha = await Repository
                .RecuperarNoTracking<Usuario>()
                .Select(detalhesUsuarioProjection)
                .FirstOrDefaultAsync(o => o.Id == id);

            return linha;
        }

        // POST api/<controller>
        [Authorize(Roles = nameof(Permissao.Administrador))]
        public void Post(InserirViewModel model)
        {
            var candidate = new Usuario
            {
                Id = model.Id,
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                Permissao = model.Permissao
            };

            var workflow = new InserirWorkflow(Repository, IdUsuario);
            workflow.Execute(candidate);
        }

        // PUT api/<controller>
        [Authorize(Roles = nameof(Permissao.Administrador) + "," + nameof(Permissao.Usuario))]
        public void Put(InserirViewModel model)
        {
            var candidate = new Usuario
            {
                Id = model.Id,
                IdUsuarioCadastrador = IdUsuario,
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                Permissao = model.Permissao
            };

            var workflow = new EditarWorkflow(Repository);
            workflow.Execute(candidate);
        }

        // DELETE api/<controller>/5
        [Authorize(Roles = nameof(Permissao.Administrador))]
        public async Task Delete(string id)
        {
            var detalhesUsuarioProjection = new DetalhesUsuarioProjection()
                .Predicate;

            var candidate = await Repository
                .Recuperar<Usuario>()
                .FirstOrDefaultAsync(o => o.Id == id);

            var workflow = new RemoverWorkflow(Repository);
            workflow.Execute(candidate);
        }
    }
}