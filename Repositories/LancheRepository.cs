using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories
{
    public class LancheRepository : ILancheRepository //Classe Concreta
    {
        private readonly AppDbContext _context; //somente Leitura
        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c=> c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.//obtendo Todos os lanches
             Where(l => l.IsLanchePreferido)//Filtrando o Lanche Preferido
            .Include(c => c.Categoria); //Obtendo As Categorias do Lanche

        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l=> l.LancheId == lancheId); 
        }
    }
}
