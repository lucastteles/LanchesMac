using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Servicos
{
    public class RelatorioVendasServico
    {
        private readonly AppDbContext context;

        public RelatorioVendasServico(AppDbContext _context)
        {
            context = _context;
        }
                         //Lista de Pedidos                    //data incial     //data Final
        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxteDate)
        {
            var resultado = from obj in context.Pedidos select obj; //Consulta

            if (minDate.HasValue) //Verificar se foi Informado uma data Minima - se for informado
            {
                resultado = resultado.Where(x=> x.PedidoEnviado >= minDate.Value);
            }
            if (maxteDate.HasValue)
            {
                resultado = resultado.Where(x=> x.PedidoEnviado <= maxteDate.Value);
            }

            return await resultado
                .Include(l=> l.PedidoItens)
                .ThenInclude(l=> l.Lanche)
                .OrderByDescending(x=> x.PedidoEnviado)//ordena pela data do pedido
                .ToListAsync(); //obtem os dados na base de dados
        }
    }
}
