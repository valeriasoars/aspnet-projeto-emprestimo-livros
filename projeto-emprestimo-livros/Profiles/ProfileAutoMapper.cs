using AutoMapper;
using projeto_emprestimo_livros.Dto.Endereco;
using projeto_emprestimo_livros.Dto.Livros;
using projeto_emprestimo_livros.Models;

namespace projeto_emprestimo_livros.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<LivroCriacaoDto, LivroModel>();
            CreateMap<LivroModel, LivroEdicaoDto>();
            CreateMap<LivroEdicaoDto, LivroModel>();
            CreateMap<EnderecoModel, EnderecoEdicaoDto>();
            CreateMap<EnderecoEdicaoDto, EnderecoModel>();
        }
    }
}
