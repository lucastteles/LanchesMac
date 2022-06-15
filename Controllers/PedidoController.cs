using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }


        [Authorize]//somente usários autorizados(logados)
        [HttpPost]

        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //obtem os Itens do Carrinho de compra do cliente
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();//obtem itens do carrinho
            _carrinhoCompra.CarrinhoCompraItems = items;

            //Verificar se Existem Itens de Pedido
            if(_carrinhoCompra.CarrinhoCompraItems.Count == 0)//se carrinho estiver vazio
            {
                ModelState.AddModelError("", "Seu carrinho esta vazio, que tal incluir um lanche...");//Mensagem de erro
            }
            //Se tiver itens no Pedido
            //calcula o total de itens e o total do pedido
            foreach(var item in items)//representa os itens do pedido
            {
                totalItensPedido += item.Quantidade;//atribui o valor da direita ao da esquerda
                precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
            }

            //atribui os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal= precoTotalPedido;


            //valida os dados do pedido
            if (ModelState.IsValid) //Se os dados do pedido forem validos
            {
                //cria o pedido e os detalhes
                _pedidoRepository.CriarPedido(pedido);


                //define mensagens ao cliente   
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();


                //limpa o carrinho do cliente
                _carrinhoCompra.LimparCarrinho();

                //exibe a view com dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            } 
            return View(pedido);
        }

    }
}
