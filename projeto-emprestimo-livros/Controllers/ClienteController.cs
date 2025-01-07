using Microsoft.AspNetCore.Mvc;
using projeto_emprestimo_livros.Services.UsuarioService;

namespace projeto_emprestimo_livros.Controllers
{
    public class ClienteController : Controller
    {
        public readonly IUsuario _usuarioInterface;

        public ClienteController(IUsuario usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }
        public async Task<ActionResult> Index(int? id)
        {
            var clientes = await _usuarioInterface.BuscarUsuarios(id);
            return View(clientes);
        }
    }
}
