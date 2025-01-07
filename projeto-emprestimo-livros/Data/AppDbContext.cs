using Microsoft.EntityFrameworkCore;
using projeto_emprestimo_livros.Models;

namespace projeto_emprestimo_livros.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<EnderecoModel> Enderecos{ get; set; }
    }


}
