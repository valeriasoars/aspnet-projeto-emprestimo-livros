using Microsoft.AspNetCore.Mvc;
using projeto_emprestimo_livros.Dto.Livros;
using projeto_emprestimo_livros.Models;
using projeto_emprestimo_livros.Services.LivroService;

namespace projeto_emprestimo_livros.Controllers
{
    public class LivroController : Controller
    {

        private readonly ILivro _livroInterface;
        public LivroController(ILivro livroInterface)
        {
            _livroInterface = livroInterface;
        }
        public async  Task<ActionResult<List<LivrosModel>>> Index()
        {
            var livros = await _livroInterface.BuscarLivros();
            return View(livros);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            if(foto != null)
            {
                if(ModelState.IsValid)
                {
                    if (!_livroInterface.VerificaSeJaExisteCadastro(livroCriacaoDto))
                    {
                        TempData["MensagemErro"] = "Código ISBN já cadastrado!";
                        return View(livroCriacaoDto);
                    }

                    var livro = await _livroInterface.Cadastrar(livroCriacaoDto, foto);
                    TempData["MensagemSucesso"] = "Livro cadastrado com sucesso!";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["MensagemErro"] = "Verifique os dados preenchidos!";
                    return View(livroCriacaoDto);
                }

            }
            else
            {
                TempData["MensagemErro"] = "Incluir uma imagem de capa!";
                return View(livroCriacaoDto);
            }
        }
    }
}
