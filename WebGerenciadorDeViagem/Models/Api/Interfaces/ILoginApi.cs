namespace GerenciadorDeViagem.WEB.Models.Api.Interfaces
{
    public interface ILoginApi
    {
        Task<UsuarioLogin> Login(UsuarioLogin usuarioLogin);
        Task<UsuarioLogin> AlterarSenha(int matricula, string senha);
    }
}
