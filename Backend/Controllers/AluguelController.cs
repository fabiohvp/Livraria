using Backend.Models.Aluguel;
using Domain.Models;
using LinqKit;
using Repository;
using Services.Projections.Alugueis;
using Services.Workflows.Alugueis;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Backend.Controllers
{
    [Authorize]
    public class AluguelController : BaseController
    {
        public AluguelController()
            : base(new LivrariaRepository(new LivrariaContext())) //O ideal seria usar Injeção de dependência mas não vou ter tempo de configurar
        {
        }

        // GET api/<controller>
        public async Task<IEnumerable<object>> Get(int page = 0, int pageSize = 10)
        {
            var detalhesLivroProjection = new DetalhesAluguelProjection()
                .Predicate;

            var linhas = await Repository
                .RecuperarNoTracking<Aluguel>()
                .AsExpandable()
                .Select(detalhesLivroProjection)
                .OrderByDescending(o => o.DataLocacao)
                .Skip(page)
                .Take(pageSize)
                .ToListAsync();

            return linhas;
        }

        // GET api/<controller>/5
        public async Task<object> Get(string id)
        {
            var detalhesLivroProjection = new DetalhesAluguelProjection()
                .Predicate;

            var aluguel = await Repository
                .RecuperarNoTracking<Aluguel>()
                .AsExpandable()
                .Select(detalhesLivroProjection)
                .FirstAsync(o => o.Id == id);

            return aluguel;
        }

        // POST api/<controller>
        [Route("api/alugar")]
        public void Alugar(AlugarViewModel model)
        {
            var aluguel = new Aluguel
            {
                IdLivro = model.IdLivro,
                IdUsuario = IdUsuario,
                QuantidadeDias = model.QuantidadeDias
            };

            var workflow = new AlugarWorkflow(Repository);
            workflow.Execute(aluguel);
        }

        // POST api/<controller>
        [Route("api/alugar")]
        public void Devolver(AlugarViewModel model)
        {
            var aluguel = new Aluguel
            {
                IdLivro = model.IdLivro,
                IdUsuario = IdUsuario,
                ValorPago = model.ValorPago,
            };

            var workflow = new DevolverWorkflow(Repository);
            workflow.Execute(aluguel);
        }
    }
}