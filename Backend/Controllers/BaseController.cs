using Microsoft.AspNet.Identity;
using Repository;
using System.Web.Http;

namespace Backend.Controllers
{
    public class BaseController : ApiController
    {
        //O ideal seria usar Injeção de dependência mas não vou ter tempo de configurar
        protected IRepository Repository;
        protected string IdUsuario => RequestContext.Principal.Identity.GetUserId();

        public BaseController(IRepository repository)
        {
            Repository = repository;
        }

        protected override void Dispose(bool disposing)
        {
            Repository.Dispose();
            base.Dispose(disposing);
        }
    }
}