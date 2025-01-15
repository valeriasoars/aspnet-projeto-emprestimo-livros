using Microsoft.AspNetCore.Mvc;
using projeto_emprestimo_livros.Dto.Usuario;
using projeto_emprestimo_livros.Enums;
using projeto_emprestimo_livros.Models;
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

        [HttpGet]
        public ActionResult Cadastrar(int? id)
        {
            ViewBag.Perfil = PerfilEnum.Administrador;

            if(id != null)
            {
                ViewBag.Perfil = PerfilEnum.Cliente;
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(int? id)
        {
            if(id != null)
            {
                var usuario = await _usuarioInterface.BuscarUsuarioPorId(id);
                return View(usuario);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            if(ModelState.IsValid)
            {
                // vai entrar no trecho apenas quando não for possivel cadastrar 
                // se retornar false, converte pra true e entra no trecho
                if (!await _usuarioInterface.VerificaSeExisteUsuarioEEmail(usuarioCriacaoDto))
                {
                    TempData["MensagemErro"] = "Já existe email/usuário cadastrado!";
                    return View(usuarioCriacaoDto);
                }

                // cadastra usuario

                var usuario = await _usuarioInterface.Cadastrar(usuarioCriacaoDto);

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

                if(usuario.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }

                return RedirectToAction("Index", "Cliente", new { Id = "0" });


            }
            else
            {
                TempData["MensagemErro"] = "Verifique os dados informados!";
                return View(usuarioCriacaoDto);
            }
        }



        [HttpPost]
        public async Task<ActionResult> MudarSituacaoUsuario(UsuarioModel usuario)
        {
            if (usuario.Id != 0 && usuario.Id != null)
            {
                var usuarioBanco = await _usuarioInterface.MudarSituacaoUsuario(usuario.Id);

                if (usuarioBanco.Situacao == true)
                {
                    TempData["MensagemSucesso"] = "Usuario ativado com sucesso";
                }
                else
                {
                    TempData["MensagemSucesso"] = "Inativação realizada com sucesso";
                }

                if (usuarioBanco.Perfil != PerfilEnum.Cliente)
                {
                    return RedirectToAction("Index", "Funcionario");
                }
                else
                {
                    return RedirectToAction("Index", "Cliente", new { Id = "0" });
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
