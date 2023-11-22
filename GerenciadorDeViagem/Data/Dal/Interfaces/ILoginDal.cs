using GerenciadorDeViagem.Model;

namespace GerenciadorDeViagem.Data.Dal.Interfaces
{
    public interface ILoginDal
    {
        Task<UsuarioLogin> ValidaCredenciais(UsuarioLogin usuarioLogin);
        Task<bool> AlterarSenha(int matricula, string senha, string novaSenha);
    }
}
