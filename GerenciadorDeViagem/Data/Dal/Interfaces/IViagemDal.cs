using GerenciadorDeViagem.Model;
using static GerenciadorDeViagem.Data.Dao.ViagemDal;

namespace GerenciadorDeViagem.Data.Dal.Interfaces
{
    public interface IViagemDal
    {
        Task<List<ViagemConsulta>> ConsultaViagem(int matricula);
        Task<bool> CadastrarViagem(Viagem viagem);
        Task<bool> CancelarViagem(int IdViagem);
        Task<bool> AprovarViagem(int IdViagem);
    

    }
}
