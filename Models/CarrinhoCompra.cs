using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }
        public static CarrinhoCompra GetCarrinho(IServiceProvider service)
        {
                //define Sessão
                ISession session= 
                service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
                
                //obtem um serviço do tipo do nosso Contexto
                var context = service.GetService<AppDbContext>();
                 
                //obtem ou gera o Id do Carrinho 
                string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

                //atribui o Id do Carrinho na Sessão
                session.SetString("CarrinhoId", carrinhoId);    

            return new CarrinhoCompra (context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }
        public void AdicionarAoCarrinho (Lanche lanche)
        {
            //verificar se Item Já existe
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                s=> s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId);
            //Se não exitir (incluir ele no Carrinho)
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade =1
                };
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            //Se ele Existir aumenta a Quantidade
            else
            {
                carrinhoCompraItem.Quantidade ++;
            }
            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault (
                s=> s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if(carrinhoCompraItem != null)
            {
                if (carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
             return CarrinhoCompraItems ??
                    (CarrinhoCompraItems = 
                 _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(s=> s.Lanche)
                .ToList());
        }
        public void LimparCarrinho()
        {
            //obter Carrinho
            var carrinhoItens = _context.CarrinhoCompraItens
                                .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
            //remover itens selecionados
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            //salvar
            _context.SaveChanges();
        }
        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId) //Buscar na lista
                        .Select(c => c.Lanche.Preco * c.Quantidade).Sum(); //selecionar e mutíplicar e somar (resultado Total)
            return total;
        }
    }
}
