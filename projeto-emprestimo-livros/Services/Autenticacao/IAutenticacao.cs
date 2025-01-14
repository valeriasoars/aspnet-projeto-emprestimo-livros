namespace projeto_emprestimo_livros.Services.Autenticacao
{
    public interface IAutenticacao
    {
        public void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
    }
}
