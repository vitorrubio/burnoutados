using AgendaBeleza.Dados;
using AgendaBeleza.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaBeleza.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly Contexto _contexto;

        public ClientesController(Contexto ctx)
        {
            this._contexto = ctx;
        }

        [HttpPost]
        public ActionResult<Cliente> Post(Cliente cli)
        {
            _contexto.Clientes.Add(cli);

            if(_contexto.SaveChanges() > 0)
            {
                return Created($"/Clientes/{cli.Id}", cli);
            }

            return BadRequest();
        }
    }
}
