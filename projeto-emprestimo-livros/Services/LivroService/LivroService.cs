using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projeto_emprestimo_livros.Data;
using projeto_emprestimo_livros.Dto.Livros;
using projeto_emprestimo_livros.Models;

namespace projeto_emprestimo_livros.Services.LivroService
{
    public class LivroService : ILivro
    {
        private readonly AppDbContext _context;
        private string _caminhoServidor;
        private readonly IMapper _mapper;
        public LivroService(AppDbContext context, IWebHostEnvironment sistema, IMapper mapper) 
        {
            _context = context;

            // caminho para chegar no wwwroot
            _caminhoServidor = sistema.WebRootPath;

            _mapper = mapper;
        }
        public async Task<List<LivrosModel>> BuscarLivros()
        {

            try
            {
                var livros = await _context.Livros.ToListAsync();
                return livros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

        public async Task<LivrosModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            try
            {
                // garantir que cada imagem tenha um nome diferente (codigo unico)
                var codigoUnico = Guid.NewGuid().ToString();
                var nomeCaminhoDaImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + livroCriacaoDto.ISBN + ".png";

                string caminhoParaSalvarImagens = _caminhoServidor + "\\Imagem\\";


                //existe a pasta? sim -> converte em false e nao entra. nao -> converte em true e entra
                if (!Directory.Exists(caminhoParaSalvarImagens))
                {
                    Directory.CreateDirectory(caminhoParaSalvarImagens);
                }

                using(var stream = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoDaImagem))
                {
                    foto.CopyToAsync(stream).Wait();
                }

                /*  var livro = new LivrosModel
                  {
                      Titulo = livroCriacaoDto.Titulo,
                      Capa = nomeCaminhoDaImagem,
                      Autor = livroCriacaoDto.Autor,
                      Descricao = livroCriacaoDto.Descricao,
                      QuantidadeEmEstoque = livroCriacaoDto.QuantidadeEmEstoque,
                      AnoPublicacao = livroCriacaoDto.AnoPublicacao,
                      ISBN = livroCriacaoDto.ISBN,
                      Genero = livroCriacaoDto.Genero
                  };*/

                var livro = _mapper.Map<LivrosModel>(livroCriacaoDto);
                livro.Capa = nomeCaminhoDaImagem;

                _context.Add(livro);
                await _context.SaveChangesAsync();
                return livro;



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerificaSeJaExisteCadastro(LivroCriacaoDto livroCriacaoDto)
        {
            try
            {
                var livroBanco = _context.Livros.FirstOrDefault(livro => livro.ISBN == livroCriacaoDto.ISBN);

                if(livroBanco != null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
