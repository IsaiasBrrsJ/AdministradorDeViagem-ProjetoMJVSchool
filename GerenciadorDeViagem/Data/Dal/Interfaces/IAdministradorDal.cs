using GerenciadorDeViagem.Model;
using static GerenciadorDeViagem.Data.Dao.AdministradorDal;

namespace GerenciadorDeViagem.Data.Dal.Interfaces
{
    public interface IAdministradorDal
    {
        Task<bool> AtualizaUsuarioNoSistema();
        Task<bool> DeletaUsuarioNoSistema(int matricula);
        Task<DadosUsuario> ConsultarUsuarioNoSistema(int matricula);
        Task<bool> CriaUsuarioNoSistema(Usuario usuario);

        Task<bool> VerificaTipoDeUsuario(int matricula);

        Task deletarViagemDoUsarioQueVaiSerDeletado(int matricula);
    }
}
