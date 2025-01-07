using Microsoft.EntityFrameworkCore;
using projeto_emprestimo_livros.Data;
using projeto_emprestimo_livros.Models;

namespace projeto_emprestimo_livros.Services.UsuarioService
{
    public class UsuarioService : IUsuario
    {

        private readonly AppDbContext _context;
        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<UsuarioModel>> BuscarUsuarios(int? id)
        {
            try
            {
                var registros = new List<UsuarioModel>();
                if(id != null)
                {
                    registros = await _context.Usuarios.Where(cliente => cliente.Perfil == 0).Include(e => e.Endereco).ToListAsync();

                }
                else
                {
                    registros = await _context.Usuarios.Where(funcionario => funcionario.Perfil != 0).Include(e => e.Endereco).ToListAsync();
                }



                return registros;

            }
            catch(Exception ex) 
            { 
                throw new Exception(ex.Message);
            }

        }
    }
}
