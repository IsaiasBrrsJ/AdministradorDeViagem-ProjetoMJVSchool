using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using GerenciadorDeViagem.WEB.Models.EndPoints;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace GerenciadorDeViagem.WEB.Models.Api
{
    public class UsuarioApi : IUsuario
    {
        private readonly IOptions<ViagemEndPoint> _viagemEndPoint;
        private readonly IApiMetodos _apiCliente;
        public UsuarioApi(IOptions<ViagemEndPoint> viagemEndPoint, IApiMetodos apiCliente)
        {
            _viagemEndPoint = viagemEndPoint;
            _apiCliente = apiCliente;
        }

        public async Task<bool> CadastrarViagem(Viagem viagem)
        {
            var endpoint = _viagemEndPoint.Value.CadastrarViagemUrl;

            var jsonDados = JsonSerializer.Serialize(viagem);

            StringContent dados = new StringContent(jsonDados, Encoding.UTF8, "application/json");

            var resposta = await _apiCliente.Enviar(endpoint, dados);
            

            if (resposta == null)
                return false;


            var dadosRetorno = JsonSerializer.Deserialize<bool>(resposta.ToString()!);


            return dadosRetorno;
        }
        public async Task<Viagem> ObterViagemPorIdAsync(int id)
        {
            var endpoint = _viagemEndPoint.Value.ConsultarViagemPorIdUrl + id;

            var resposta = await _apiCliente.Obter(endpoint);

            if (resposta == null)
                return null!;


            var dadosRetorno = JsonSerializer.Deserialize<Viagem>(resposta.ToString()!);


            return dadosRetorno!;
        }

        public async Task<List<Viagem>> ObterViagemPorMatriculaListAsync(int matricula)
        {
       
            var endpoint = _viagemEndPoint.Value.ConsultarViagemUrl + matricula  ;

            var resposta = await _apiCliente.Obter(endpoint);

            if (resposta == null)
                return null!;

            
            var dadosRetorno = JsonSerializer.Deserialize<List<Viagem>>(resposta.ToString()!);

           
            return dadosRetorno!;
        }

        public async Task<bool> CancelarViagemAsync(int id)
        {
            var endpoint = _viagemEndPoint.Value.CancelarViagemUrl + id;

            var resposta = await _apiCliente.Atualizar(endpoint, null!);

            var dadosRetorno = JsonSerializer.Deserialize<bool>(resposta);

            return dadosRetorno;
        }

        public async Task<bool> AprovarViagemAsync(int id)
        {
            var endpoint = _viagemEndPoint.Value.AprovarViagemUrl + id;

            var respota = await _apiCliente.Atualizar(endpoint, null!);

            var dadosRetorno = JsonSerializer.Deserialize<bool>(respota);


            return dadosRetorno;
        }
    }
}
