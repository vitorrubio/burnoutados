using AgendaBeleza.Dados;
using AgendaBeleza.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

            if (_contexto.SaveChanges() > 0)
            {
                return Created($"/Clientes/{cli.Id}", cli);
            }

            return BadRequest();
        }


        [HttpGet]
        public ActionResult<List<Cliente>> Get()
        {
            return Ok(_contexto.Clientes.AsNoTracking().ToList());
        }



        [HttpGet("{id:int}")]
        public ActionResult<Cliente> Get(int id)
        {
            var cliente = _contexto.Clientes.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }



        [HttpGet("paginado/{qtdPag:int}/{pag:int}")]
        public ActionResult GetPaginado(int qtdPag = 10, int pag = 1)
        {
            if (pag <= 0 || qtdPag <= 0)
            {
                return BadRequest();
            }

            var totalClientes = _contexto.Clientes.AsNoTracking().Count();
            var totalPaginas = totalClientes / qtdPag;
            if (totalClientes % qtdPag > 0)
            {
                totalPaginas++;
            }

            if (pag > totalPaginas)
            {
                return BadRequest();
            }

            var cliente = _contexto
                .Clientes
                .OrderBy(x => x.Nome)
                .Skip((pag - 1) * qtdPag)
                .Take(qtdPag)
                .ToList();

            if (cliente == null || !cliente.Any())
            {
                return NotFound();
            }

            return Ok(new
            {
                TotalClientes = totalClientes,
                TotalPaginas = totalPaginas,
                Clientes = cliente
            });
        }



        [HttpGet("alfabetico/{txt}/{qtdPag:int}/{pag:int}")]
        public ActionResult GetPorTexto(string txt, int qtdPag = 10, int pag = 1)
        {
            if (pag <= 0 || qtdPag <= 0)
            {
                return BadRequest();
            }

            var totalClientes = _contexto.Clientes.AsNoTracking().Where(x => x.Nome.Contains(txt)).Count();
            var totalPaginas = totalClientes / qtdPag;
            if (totalClientes % qtdPag > 0)
            {
                totalPaginas++;
            }

            if (pag > totalPaginas)
            {
                return BadRequest();
            }

            var cliente = _contexto
                .Clientes
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.Apelido
                })
                .Where(x => x.Nome.Contains(txt))
                .OrderBy(x => x.Nome)
                .Skip((pag - 1) * qtdPag)
                .Take(qtdPag)
                .ToList();

            if (cliente == null || !cliente.Any())
            {
                return NotFound();
            }

            return Ok(new
            {
                TotalClientes = totalClientes,
                TotalPaginas = totalPaginas,
                Clientes = cliente
            });
        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var cli = _contexto.Clientes.Find(id);
            if (cli == null)
            {
                return NotFound();
            }

            _contexto.Clientes.Remove(cli);
            if (_contexto.SaveChanges() > 0)
            {
                return NoContent();
            }

            return BadRequest();
        }



        [HttpPatch("{id:int}")]
        public ActionResult Patch(int id, [FromBody] Cliente alteracao)
        {
            var atual = _contexto.Clientes.Find(id);
            if (atual == null)
            {
                return NotFound();
            }

            atual.Nome = alteracao.Nome;
            atual.Apelido = alteracao.Apelido;
            atual.DataNascimento = alteracao.DataNascimento;
            atual.Bloqueado = alteracao.Bloqueado;
            atual.CPF = alteracao.CPF;

            _contexto.SaveChanges();

            return Ok(atual);

        }


    }
}
