using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)//se o Usário for autentificado
            {
                return View();
            }

            return RedirectToAction("Login","Account");
           
        }
    }
}
