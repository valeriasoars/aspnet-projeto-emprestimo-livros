﻿using projeto_emprestimo_livros.Enums;
using projeto_emprestimo_livros.Models;
using System.ComponentModel.DataAnnotations;

namespace projeto_emprestimo_livros.Dto.Usuario
{
    public class UsuarioCriacaoDto
    {
        [Required(ErrorMessage = "Digite o nome completo!")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "Digite o usuário!")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Digite o e-mail!")]
        [EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Selecione o cargo!")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Selecione o turno!")]
        public TurnoEnum Turno { get; set; }
        [Required(ErrorMessage = "Digite o logradouro!")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Digite o bairro!")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Digite o número!")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Digite o CEP!")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Digite o estado!")]
        public string Estado { get; set; }
        public string? Complemento { get; set; }
        [Required(ErrorMessage = "Digite uma senha!")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres!")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Digite a confirmação da senha!")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem!")]
        public string ConfirmarSenha { get; set; }

    }
}
