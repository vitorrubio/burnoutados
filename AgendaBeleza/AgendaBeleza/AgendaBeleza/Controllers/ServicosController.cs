using AgendaBeleza.Dados;
using AgendaBeleza.Dominio;
using Microsoft.AspNetCore.Mvc;

namespace AgendaBeleza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicosController : ControllerBase
    {
        private readonly Contexto _contexto;

        public ServicosController(Contexto ctx)
        {
            this._contexto = ctx;
        }

        [HttpPost]
        public ActionResult<Servico> Post(Servico srv)
        {
            
            _contexto.Servicos.Add(srv);

            if (_contexto.SaveChanges() > 0)
            {
                return Created($"/Servicos/{srv.Id}", srv);
            }

            return BadRequest();
        }
    }
}
