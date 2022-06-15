//namespace lanchesMac;


//////////C�digo Gerado Do Visual Studio 2019/////////


//public class Program //////////C�digo Gerado Do Visual Studio 2019/////////
//{

//    public static void Main(string[] args)
//    {
//        CreateHostBuilder(args)
//            .Build()
//            .Run();
//    }

//    public static IHostBuilder CreateHostBuilder(string[] args) =>
//        Host.CreateDefaultBuilder(args)
//        .ConfigureWebHostDefaults(webBuilder =>
//        {
//            webBuilder.UseStartup<LanchesMac.Startup>();
//        });                                  //Chama a Pagina Startup
//}


using LanchesMac.Areas.Admin.Servicos;
using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

var builder = WebApplication.CreateBuilder(args);//Definindo a Instancia do Builder

builder.Services.AddDbContext<AppDbContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()//Store- para recuperar e armazenar as Informa��es de usu�rio e perfil
            .AddDefaultTokenProviders();//Alterar Informa��es

//Remove padr�o de senha do Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false; //Obriga a ter um numero na senha
                                           //options.Password.RequiredLength = 7; // valor default 8 digitos
    options.Password.RequiredUniqueChars = 6; // minimos caracteres unicos
    options.Password.RequireLowercase = false; // n�o � obrigat�rio come�ar com minusculo
    options.Password.RequireNonAlphanumeric = false; // n�o � obrigat�rio alfanumerico
    options.Password.RequireUppercase = false; // n�o � obrigatorio come�ar com maiusculo
});

builder.Services.Configure<ConfigurationImagens>(builder.Configuration
    .GetSection("ConfigurationPastaImagens"));


builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

builder.Services.AddScoped<RelatorioVendasServico>();
builder.Services.AddScoped<GraficoVendasService>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        politica =>
        {
            politica.RequireRole("Admin");
        });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//vai valer por todo o tempo de vida da Aplica��o (as informa��es v�o permanecer)
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));//Se dois Clientes solicitarem o Carrinho eles ter�o Instancias Diferente (AddScoped)

builder.Services.AddControllersWithViews();

builder.Services.AddPaging(options => //Pagina��o do adm
{
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageindex";
});

builder.Services.AddMemoryCache(); //Registrando os middes (Onde S�o Armazenados os Dados)
builder.Services.AddSession();//Recurso para Salvar e Armazenar dados do Usu�rio
    



var app = builder.Build();//Instancia do App

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

CriarPerfisUsuarios(app);
////cria os perfis
//seedUserRoleInitial.SeedRoles();
////cria os usu�rios e atribui ao pefil
//seedUserRoleInitial.SeedUserRoles(); //SeedUsersRoles

app.UseSession();
app.UseAuthentication();//Sempre Antes da Autoriza��o 
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllerRoute( //area Admin
       name: "areas",
       pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");


    endpoints.MapControllerRoute(
        name: "categoriaFiltro",
        pattern: "Lanche/{action}/{categoria?}",
        defaults: new { controller = "Lanche", Action = "List" });


    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
    






app.Run();//Execu��o do App

void CriarPerfisUsuarios (WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using(var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        service.SeedUserRoles();
        service.SeedRoles();
    }
}