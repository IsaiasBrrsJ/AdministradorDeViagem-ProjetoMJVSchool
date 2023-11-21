namespace GerenciadorDeViagem.WEB.Models.Api.Interfaces
{
    public interface ILoginApi
    {
        Task<UsuarioLogin> Login(UsuarioLogin usuarioLogin);
        Task<bool> AlterarSenha(int matricula, string senha, string novaSenha);
    }
}
