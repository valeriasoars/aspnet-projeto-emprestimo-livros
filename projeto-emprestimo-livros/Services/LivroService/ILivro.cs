using projeto_emprestimo_livros.Dto.Livros;
using projeto_emprestimo_livros.Models;

namespace projeto_emprestimo_livros.Services.LivroService
{
    public interface ILivro
    {
        Task<List<LivroModel>> BuscarLivros();
        bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto);
        Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto);
        Task<LivroModel> BuscarLivroPorId(int? id);
        Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto);
    }
}
