﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using projeto_emprestimo_livros.Dto.Livros;
using projeto_emprestimo_livros.Models;
using projeto_emprestimo_livros.Services.LivroService;

namespace projeto_emprestimo_livros.Controllers
{
    public class LivroController : Controller
    {

        private readonly ILivro _livroInterface;
        private readonly IMapper _mapper;
        public LivroController(ILivro livroInterface, IMapper mapper)
        {
            _livroInterface = livroInterface;
            _mapper = mapper;
        }
        public async  Task<ActionResult<List<LivroModel>>> Index()
        {
            var livros = await _livroInterface.BuscarLivros();
            return View(livros);
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Detalhes(int? id)
        {
            if(id != null)
            {
                var livro = await _livroInterface.BuscarLivroPorId(id);
                return View(livro);
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> Editar(int? id)
        {
            if (id != null)
            {
                var livro = await _livroInterface.BuscarLivroPorId(id);
                var livroEdicaoDto = _mapper.Map<LivroEdicaoDto>(livro);
                return View(livroEdicaoDto);
            }
            return RedirectToAction("Index");
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



        [HttpPost]
        public async Task<ActionResult> Editar(LivroEdicaoDto livroEdicaoDto, IFormFile? foto)
        {
            if (ModelState.IsValid)
            {
                var livro = await _livroInterface.Editar(livroEdicaoDto, foto);
                return RedirectToAction("Index");

            }
            else
            {
                TempData["MensagemErro"] = "Verifique os dados preenchidos!";
                return View(livroEdicaoDto);
            }
           
        }
    }
}
