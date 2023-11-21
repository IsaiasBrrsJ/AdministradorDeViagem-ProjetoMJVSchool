namespace GerenciadorDeViagem.WEB.Models.Api.Interfaces
{
    public interface IUsuario
    {
        Task<List<Viagem>> ObterViagemPorMatriculaListAsync(int matricula);
        Task<Viagem> ObterViagemPorIdAsync(int id);

        Task<bool> CancelarViagemAsync(int id);

        Task<bool> AprovarViagemAsync(int id);

        Task<bool> CadastrarViagem(Viagem viagem);
    }
}
