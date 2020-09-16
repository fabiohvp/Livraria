using Backend.Models.Aluguel;
using Domain.Models;
using Repository;
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
            var linhas = await Repository
                .Recuperar<Aluguel>()
                .AsNoTracking()
                .Select(o => new
                {
                    o.Id,
                    Livro = o.Livro.Nome,
                    Usuario = o.Usuario.Nome,
                    o.DataLocacao,
                    o.DataDevolucao
                })
                .OrderByDescending(o => o.DataLocacao)
                .Skip(page)
                .Take(pageSize)
                .ToListAsync();

            return linhas;
        }

        // GET api/<controller>/5
        public async Task<object> Get(string id)
        {
            var aluguel = await Repository
                .Recuperar<Aluguel>()
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
                ValorPago = model.ValorPago,
            };

            var alugarWorkflow = new AlugarWorkflow(Repository);
            alugarWorkflow.Execute(aluguel);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}