using System.ComponentModel.DataAnnotations;

namespace LanchesMac.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Informe o nome")]
        [Display(Name="Usuário")]
        public string UserName { get; set; }


        [Required(ErrorMessage ="informe a senha")]
        [DataType(DataType.Password)]//A senha fica Ilegível
        [Display (Name="Senha")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string Permissao { get; set; }//Outra possibilidade
    }
}
