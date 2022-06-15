//using LanchesMac.Areas.Admin.Servicos;
//using LanchesMac.Context;
//using LanchesMac.Models;
//using LanchesMac.Repositories;
//using LanchesMac.Repositories.Interfaces;
//using LanchesMac.Services;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using ReflectionIT.Mvc.Paging;

//namespace LanchesMac;
////Código Gerado do Visual Studio 2019 e algumas Coisas foram Apagadas
//public class Startup
//{
//    public Startup(IConfiguration configuration)
//    {
//        Configuration = configuration;
//    }

//    public IConfiguration Configuration { get; }

//    // This method gets called by the runtime. Use this method to add services to the container.
//    public void ConfigureServices(IServiceCollection services)
//    {
//        services.AddDbContext<AppDbContext>(options =>
//         options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


//        //Login Autentificação 
//                      //gerenciar Usuário    //gerenciar Perfis
//        services.AddIdentity<IdentityUser, IdentityRole>()
//            .AddEntityFrameworkStores<AppDbContext>()//Store- para recuperar e armazenar as Informações de usuário e perfil
//            .AddDefaultTokenProviders();//Alterar Informações

//        //Remove padrão de senha do Identity
//        services.Configure<IdentityOptions>(options =>
//        {
//            options.Password.RequireDigit = false; //Obriga a ter um numero na senha
//                                                   //options.Password.RequiredLength = 7; // valor default 8 digitos
//            options.Password.RequiredUniqueChars = 6; // minimos caracteres unicos
//            options.Password.RequireLowercase = false; // não é obrigatório começar com minusculo
//            options.Password.RequireNonAlphanumeric = false; // não é obrigatório alfanumerico
//            options.Password.RequireUppercase = false; // não é obrigatorio começar com maiusculo
//        });

//        services.Configure<ConfigurationImagens>
//            (Configuration.GetSection("ConfigurationPastaImagens"));


//        services.AddTransient<ILancheRepository, LancheRepository>();
//        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
//        services.AddTransient<IPedidoRepository, PedidoRepository>();
//        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

//        services.AddScoped<RelatorioVendasServico>();
//        services.AddScoped<GraficoVendasService>();


//        services.AddAuthorization(options =>
//        {
//            options.AddPolicy("Admin",
//                politica =>
//                {
//                    politica.RequireRole("Admin");
//                });
//        });

//        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//vai valer por todo o tempo de vida da Aplicação (as informações vão permanecer)
//        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));//Se dois Clientes solicitarem o Carrinho eles terão Instancias Diferente (AddScoped)
            
//        services.AddControllersWithViews();

//        services.AddPaging(options => //Paginação do adm
//        {
//            options.ViewName = "Bootstrap4";
//            options.PageParameterName = "pageindex";
//        });

//        services.AddMemoryCache(); //Registrando os middes (Onde São Armazenados os Dados)
//        services.AddSession();//Recurso para Salvar e Armazenar dados do Usuário
//    }

//    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//    public void Configure(IApplicationBuilder app,
//        IWebHostEnvironment env,ISeedUserRoleInitial seedUserRoleInitial)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseDeveloperExceptionPage();
//        }
//        else
//        {
//            app.UseExceptionHandler("/Home/Error");
//            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//            app.UseHsts();
//        }
//        app.UseHttpsRedirection();

//        app.UseStaticFiles();
//        app.UseRouting();


//        //cria os perfis
//        seedUserRoleInitial.SeedRoles();
//        //cria os usuários e atribui ao pefil
//        seedUserRoleInitial.SeedUserRoles(); //SeedUsersRoles

//        app.UseSession();
//        app.UseAuthentication();//Sempre Antes da Autorização 
//        app.UseAuthorization();

//        app.UseEndpoints(endpoints =>
//        {
           
//             endpoints.MapControllerRoute( //area Admin
//                name: "areas",
//                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
            

//            endpoints.MapControllerRoute(
//                name:"categoriaFiltro",
//                pattern: "Lanche/{action}/{categoria?}",
//                defaults: new {controller="Lanche", Action="List"});


//            endpoints.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");
//        });
//    }
//}