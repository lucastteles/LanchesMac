using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Categorias")]

    public class Categoria
    {
        [Key]//CategoriaId
        public int CategoriaId{ get; set; }


        //Categoria Nome
        [StringLength(100, ErrorMessage ="O tamanho máximo é 100 caracteres")]
        [Required(ErrorMessage = "informe o nome da Categoria")]
        [Display(Name ="Nome")]
        public string CategoriaNome { get; set; }


        //Descrição
        [StringLength(200, ErrorMessage ="O tamanho máximo é 200 Caracteres")]
        [Required(ErrorMessage = "Informe a descrição da Categoria")]
        [Display(Name ="Descrição")]
        public string Descricao { get; set; }


        public List<Lanche> Lanches { get; set; }
    }
}
