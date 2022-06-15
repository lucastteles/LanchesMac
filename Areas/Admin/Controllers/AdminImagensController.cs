using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagensController(IWebHostEnvironment hostingEnvironment,
                    IOptions<ConfigurationImagens> myConfiguration)
        {
            _hostingEnvironment = hostingEnvironment;
            _myConfig = myConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
        //Permite Enviar Arquivo
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            if(files == null || files.Count == 0)
            {
                ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                return View(ViewData);
            }

            if (files.Count > 10)
            {
                ViewData["Erro"] = "Erro: Quantidade de Arquivos excedeu o limite";
                return View(ViewData);
            }

            long size = files.Sum(f => f.Length); //para calcular o total 

            var filePathsName = new List<string>(); //Armazernar os nomes dos arquivos q foram enviados

            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, //local onde vou armazenar as imagens
                 _myConfig.NomePastaImagensProdutos); 

            foreach(var formFile in files) //Percorre cada arquivo q foi selecionado
            {
                if (formFile.FileName.Contains(".jpeg") || formFile.FileName.Contains(".gif") || formFile.FileName.Contains(".png"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);    

                    filePathsName.Add(fileNameWithPath);
                                                                         //se n exitir ele vai salvar. Se existir ele vai sobreescrever
                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Resultado"] = $"{files.Count} arquivos foram enviados ao servidor, " +
                                     $"com tamanho total de {size} bytes";

            ViewBag.Arquivos = filePathsName;

            return View(ViewData);

        }

        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NomePastaImagensProdutos); //caminho da imagem


            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagesProduto = _myConfig.NomePastaImagensProdutos;

            if(files.Length == 0) // se n possui arquivo 
            {
                ViewData["Erro"] = $"Nenhum rquivo encontrado na pasta {userImagesPath}";
            }

            model.Files = files; // se tiver

            return View(model);
        }

        public IActionResult Deletefile(string fname)
        {
            string _imagemDeleta = Path.Combine(_hostingEnvironment.WebRootPath,_myConfig.NomePastaImagensProdutos + "\\", fname);

            if ((System.IO.File.Exists(_imagemDeleta))) //se ele existe
            {
                System.IO.File.Delete(_imagemDeleta);//Deletando Imagem

                ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} deletado com sucesso";
            }

            return View("index");
        }
    }
}
