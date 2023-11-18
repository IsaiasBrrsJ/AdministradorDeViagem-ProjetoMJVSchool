namespace GerenciadorDeViagem.WEB.Models.Api.Interfaces
{
    public interface IApiMetodos
    {
        Task<Object> Obter(string endpoint);
        Task<Object> Enviar(string endpoint, StringContent dados);
        Task Deletar(string endpoint);
        Task Atualizar(string endpoint);
    }
}
