using Microsoft.AspNetCore.Mvc;
using projeto_emprestimo_livros.Services.UsuarioService;

namespace projeto_emprestimo_livros.Controllers
{
    public class UsuarioController : Controller
    {
        public readonly IUsuario _usuarioInterface;
        public UsuarioController(IUsuario usuario_interface)
        {
            _usuarioInterface = usuario_interface;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios(id);
            return View();
        }
    }
}
