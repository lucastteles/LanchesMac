namespace LanchesMac.Models
{
    public class FileManagerModel
    {
        public FileInfo[] Files { get; set; }//Acesso a metodos e propiedades para tratar os arquivos
        public IFormFile IFormFile { get; set; }//interface q permite eu enviar os arquivos temdo diversas informações do arquivo
        public List<IFormFile> IFormFiles { get; set;}//Lista de arquivos
        public string PathImagesProduto { get; set; }//Armazenar o nome da pasta no servidor
    }
}
