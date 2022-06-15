using LanchesMac.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendasServico relatorioVendasServico;

        public AdminRelatorioVendasController(RelatorioVendasServico _relatorioVendasServico)
        {
            relatorioVendasServico = _relatorioVendasServico;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate,
            DateTime? maxDate)
        {
            if (!minDate.HasValue) //se n for informado
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate= DateTime.Now;
            }

            ViewData["minData"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxData"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await relatorioVendasServico.FindByDateAsync(minDate,maxDate);
            return View(result);
        }
    }
}
