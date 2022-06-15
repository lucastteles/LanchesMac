using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {

        //Chave
        [Key]
        public int LancheId { get; set; }


        //Nome do Lanche
        [Required(ErrorMessage = "O nome do lanche deve ser Iformado")]
        [Display(Name ="Nome do Lanche")]
        [StringLength(80, MinimumLength =  10, ErrorMessage ="O {0} deve ter no minimo {1} e no máximo{2}")]
        public string Nome { get; set; }


        //Descrição Curta
        [Required(ErrorMessage = "A Descrição do lanche deve ser Infomada")]
        [Display(Name ="Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no Mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage ="Descrição não pode exceder {1} caracteres")]
        public string DescricaoCurta { get; set; }


        //Descrição Detalhada
        [Required(ErrorMessage ="A descrição detalhada deve ser informada")]
        [Display(Name ="Descrição detalhada do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição deve ter no Mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage ="Descrição não pode exceder {1} caracteres")]
        public string DescricaoDetalhada { get; set; }


        //Preço
        [Required(ErrorMessage ="O preço deve ser Informado")]
        [Display (Name ="Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1,999.99, ErrorMessage ="O Preço deve estar enter 1 e 999,99")]
        public decimal Preco { get; set; }


        //Imagem Url
        [Display(Name ="Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage ="O {0} deve ter no Máximo {1} caracteres")]
        public string imagemUrl { get; set; }


        //Imagem ThumbnailUrl
        [Display(Name ="Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage="O {0} deve ter no Máximo {1} caracteres")]
        public string imagemThumbnailUrl { get; set; }


        //Lanche Preferido
        [Display (Name ="Preferido ?")]
        public bool IsLanchePreferido { get; set; }


        //Em Estoque
        [Display(Name ="Estoque")]
        public bool EmEstoque { get; set; }


        [Display(Name = "Categorias")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
