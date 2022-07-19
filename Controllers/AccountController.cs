using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signManager = signManager;
           // _roleManager = roleManager;
        }

        //[HttpGet]  //método login
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
              
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginVM)////
        {
            if (!ModelState.IsValid) //se não for valido
                return View(loginVM); //retorna a pagina de login

            //Se for Valido
            var user = await signManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
            if (user.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Falha ao realizar o login !!");
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(LoginViewModel resgistroVM)///////
        {
            //Verfica se o ModelState é valido
            if (ModelState.IsValid)
            {
                //se for valido
                var user = new IdentityUser { UserName = resgistroVM.UserName, Email = resgistroVM.UserName};
                var result = await userManager.CreateAsync(user, resgistroVM.Password); 

                //se o resultado foi feito com sucesso
                if (result.Succeeded)
                {
                    //Para adicionar uma permissão
                    await AdicionarPermissao(resgistroVM, user);

                    // await signManager.SignInAsync(user, isPersistent: false);
                    await userManager.AddToRoleAsync(user, "Member");
                    return RedirectToAction("Login", "Account");
                }
                // Se não deu certo
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usário");
                }
            }

            return View(resgistroVM);
        }

        private async Task AdicionarPermissao(LoginViewModel usuarioVm, IdentityUser user)
        {
            var applicationRole = await _roleManager.FindByNameAsync(usuarioVm.Permissao);
            if (applicationRole != null)
            {
                await userManager.AddToRoleAsync(user, applicationRole.Name);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
