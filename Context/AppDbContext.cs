using LanchesMac.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//Carrega Informações De Configuração do DbContext
//Define Quias são as classes q estou Mapeando
namespace LanchesMac.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>// Login //Aqui é feita as definiçoes do Identity para as Tabelas
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }   
        // Mapeamento das Tabelas

                    //classe     //tabela
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; } 
    }
}
