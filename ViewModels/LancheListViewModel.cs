using LanchesMac.Models;

namespace LanchesMac.ViewModels
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> Lanches { get; set; }//propiedade para exibir uma lista de Lanches

        public string CategoriaAtual { get; set; }
    }
}
