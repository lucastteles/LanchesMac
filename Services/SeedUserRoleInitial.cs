using Microsoft.AspNetCore.Identity;

namespace LanchesMac.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()//Criação do perfil
        {
            if (!_roleManager.RoleExistsAsync("Member").Result) //Se esse perifil NÃO existe
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member"; //nome da role q vou criar
                role.NormalizedName = "MEMBER"; //msm nome só q em caixa alta
                IdentityResult roleResult =_roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;  
            }
        }

        public void SeedUserRoles()//Criar os usários e atribuir os usários aos perfis
        {
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)//procurar o usario com o metodo FindBY
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)//verificar se essa operação foi feita com sucesso(foi criado)
                {
                    _userManager.AddToRoleAsync(user,"Member").Wait();    
                }
            }

            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)//procurar o usario com o metodo FindBY
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMINLOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = _userManager.CreateAsync(user, "Numsey#2022").Result;

                if (result.Succeeded)//verificar se essa operação foi feita com sucesso(foi criado)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
