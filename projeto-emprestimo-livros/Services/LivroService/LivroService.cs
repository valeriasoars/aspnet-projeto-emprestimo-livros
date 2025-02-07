﻿using AutoMapper;
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
        public async Task<List<LivroModel>> BuscarLivros()
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

        public async Task<LivroModel> Cadastrar(LivroCriacaoDto livroCriacaoDto, IFormFile foto)
        {
            try
            {
                var nomeCaminhoDaImagem = GeraCaminhoArquivo(foto);

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

                var livro = _mapper.Map<LivroModel>(livroCriacaoDto);
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

        public async Task<LivroModel> BuscarLivroPorId(int? id)
        {
            try
            {
               var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);
                return livro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LivroModel> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile foto)
        {
            try
            {
                var livro = await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == livroEdicaoDto.Id);
                var nomeCaminhoDaImagem = "";
                if (foto != null)
                {
                    // deletando a imagem que foi editada
                    string caminhoCapaExistente = _caminhoServidor + "\\Imagem\\" + livro.Capa;
                    if (File.Exists(caminhoCapaExistente))
                    {
                        File.Delete(caminhoCapaExistente);
                    }

                    nomeCaminhoDaImagem = GeraCaminhoArquivo(foto);
                }

               var livrosModel = _mapper.Map<LivroModel>(livroEdicaoDto);

                if(nomeCaminhoDaImagem != "")
                {
                    livrosModel.Capa = nomeCaminhoDaImagem;
                }
                else
                {
                    livrosModel.Capa = livro.Capa;
                }

                livrosModel.DataDeAlteracao = DateTime.Now;
                _context.Update(livrosModel);
                await _context.SaveChangesAsync();
                return livrosModel;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string GeraCaminhoArquivo(IFormFile foto)
        {
            // garantir que cada imagem tenha um nome diferente (codigo unico)
            var codigoUnico = Guid.NewGuid().ToString();
            var nomeCaminhoDaImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico +  ".png";

            string caminhoParaSalvarImagens = _caminhoServidor + "\\Imagem\\";


            //existe a pasta? sim -> converte em false e nao entra. nao -> converte em true e entra
            if (!Directory.Exists(caminhoParaSalvarImagens))
            {
                Directory.CreateDirectory(caminhoParaSalvarImagens);
            }

            using (var stream = System.IO.File.Create(caminhoParaSalvarImagens + nomeCaminhoDaImagem))
            {
                foto.CopyToAsync(stream).Wait();
            }

            return nomeCaminhoDaImagem;
        }
    }
}
