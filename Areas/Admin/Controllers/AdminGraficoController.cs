using LanchesMac.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGraficoController : Controller
    {
        private readonly GraficoVendasService _graficoVendas;

        public AdminGraficoController(GraficoVendasService graficoVendas)
        {
            _graficoVendas = graficoVendas;
        }

        public JsonResult VendasLanches(int dias)
        {
            var lanchesVendasTotais = _graficoVendas.GetVendasLanches(dias);

            return Json(lanchesVendasTotais);
        }

        [HttpGet]
        public IActionResult Index()//Calcular venda dos 360 dias
        {
            return View();
        }


        [HttpGet]
        public IActionResult VendasMensal()//Calcular venda dos 30dias
        {
            return View();
        }


        [HttpGet]
        public IActionResult VendasSemanal()//Calcular venda dos 7 dias
        {
            return View();
        }
    }
}
