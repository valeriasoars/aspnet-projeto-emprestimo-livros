using Microsoft.AspNetCore.Mvc;
using projeto_emprestimo_livros.Services.UsuarioService;

namespace projeto_emprestimo_livros.Controllers
{
    public class FuncionarioController : Controller
    {
        public readonly IUsuario _usuarioInterface;

        public FuncionarioController(IUsuario usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }
        public async Task<ActionResult> Index()
        {
            var funcionarios = await _usuarioInterface.BuscarUsuarios(null);
            return View(funcionarios);
        }
    }
}
