using AgendaBeleza.Dados;
using AgendaBeleza.Dominio;
using AgendaBeleza.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPut]
        public ActionResult<Servico> Put(Servico srv)
        {
            
            _contexto.Servicos.Add(srv);

            if (_contexto.SaveChanges() > 0)
            {
                return Created($"/Servicos/{srv.Id}", srv);
            }

            return BadRequest();
        }








        [HttpGet]
        public ActionResult<List<Servico>> Get()
        {
            return Ok(_contexto.Servicos.AsNoTracking().ToList());
        }



        [HttpGet("{id:int}")]
        public ActionResult<Servico> Get(int id)
        {
            var srvcs = _contexto.Servicos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (srvcs == null)
            {
                return NotFound();
            }

            return Ok(srvcs);
        }



        [HttpGet("paginado/{qtdPag:int}/{pag:int}")]
        public ActionResult GetPaginado(int qtdPag = 10, int pag = 1)
        {
            if (pag <= 0 || qtdPag <= 0)
            {
                return BadRequest("Pagina ou qtd não pode ser menor ou igual a zero");
            }

            var totalSvcs = _contexto.Servicos.AsNoTracking().Count();
            var totalPaginas = totalSvcs / qtdPag;
            if (totalSvcs % qtdPag > 0)
            {
                totalPaginas++;
            }

            if (pag > totalPaginas)
            {
                return BadRequest("a página não pode ser maior que a qtde de páginas");
            }

            var svcs = _contexto
                .Servicos
                .OrderBy(x => x.Nome)
                .Skip((pag - 1) * qtdPag)
                .Take(qtdPag)
                .ToList();

            if (svcs == null || !svcs.Any())
            {
                return NotFound();
            }

            return Ok(new
            {
                TotalServicos = totalSvcs,
                TotalPaginas = totalPaginas,
                Servicos = svcs
            });
        }



        [HttpGet("alfabetico/{txt}/{qtdPag:int}/{pag:int}")]
        public ActionResult GetPorTexto(string txt, int qtdPag = 10, int pag = 1)
        {
            if (pag <= 0 || qtdPag <= 0)
            {
                return BadRequest("Pagina ou qtd não pode ser menor ou igual a zero");
            }

            var totalSvc = _contexto.Servicos.AsNoTracking().Where(x => x.Nome.Contains(txt)).Count();
            var totalPaginas = totalSvc / qtdPag;
            if (totalSvc % qtdPag > 0)
            {
                totalPaginas++;
            }

            if (pag > totalPaginas)
            {
                return BadRequest("a página não pode ser maior que a qtde de páginas");
            }

            var svcs = _contexto
                .Servicos
                .Select(x => new
                {
                    x.Id,
                    x.Nome,
                    x.Preco
                })
                .Where(x => x.Nome.Contains(txt))
                .OrderBy(x => x.Nome)
                .Skip((pag - 1) * qtdPag)
                .Take(qtdPag)
                .ToList();

            if (svcs == null || !svcs.Any())
            {
                return NotFound();
            }

            return Ok(new
            {
                TotalServicos = totalSvc,
                TotalPaginas = totalPaginas,
                Servicos = svcs
            });
        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var servico = _contexto.Servicos.Find(id);
            if (servico == null)
            {
                return NotFound();
            }

            _contexto.Servicos.Remove(servico);

            if (_contexto.SaveChanges() > 0)
            {
                return NoContent();
            }

            return BadRequest();

        }



        [HttpPatch("{id:int}")]
        public ActionResult Patch(int id, [FromBody] Servico alteracao)
        {
            var atual = _contexto.Servicos.Find(id);
            if (atual == null)
            {
                return NotFound();
            }

            atual.Nome = alteracao.Nome;
            atual.DuracaoMinutos = alteracao.DuracaoMinutos;
            atual.Disponivel = alteracao.Disponivel;
            atual.Preco = alteracao.Preco;

            _contexto.SaveChanges();

            return Ok(atual);

        }

    }

}
