﻿using System.ComponentModel.DataAnnotations;

namespace projeto_emprestimo_livros.Dto.Livros
{
    public class LivroEdicaoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Insira um título!")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira uma descrição!")]

        public string Descricao { get; set; } = string.Empty;

        
        public string? Capa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Insira o código ISBN!")]
        public string ISBN { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o nome do autor!")]
        public string Autor { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o genero!")]
        public string Genero { get; set; } = string.Empty;
        [Required(ErrorMessage = "Insira o ano de publicação!")]
        public int AnoPublicacao { get; set; }
        [Required(ErrorMessage = "Insira uma quantidade em estoque!")]
        public int QuantidadeEmEstoque { get; set; }
    }
}
