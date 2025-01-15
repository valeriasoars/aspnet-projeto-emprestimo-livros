using projeto_emprestimo_livros.Dto.Usuario;
using projeto_emprestimo_livros.Models;

namespace projeto_emprestimo_livros.Services.UsuarioService
{
    public interface IUsuario
    {
        Task<List<UsuarioModel>> BuscarUsuarios(int? id);
        Task<bool> VerificaSeExisteUsuarioEEmail(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<UsuarioCriacaoDto> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto);
        Task<UsuarioModel> BuscarUsuarioPorId(int? id);
        Task<UsuarioModel> MudarSituacaoUsuario(int id);
        Task<UsuarioModel> Editar(UsuarioEdicaoDto usuarioEdicaoDto);
    }
}
