using Microsoft.AspNetCore.Mvc;
using MvcTradicional.Models;
using System.Diagnostics;

namespace MvcTradicional.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View("Index");
        }



        [HttpPost]
        public IActionResult Salvar(Produto prod)
        {
            Produto.BdProdutos.Add(prod);
            //return View("Index");
            return PartialView(prod);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}