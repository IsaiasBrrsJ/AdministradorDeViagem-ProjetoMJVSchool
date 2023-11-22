using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using GerenciadorDeViagem.WEB.Models.EndPoints;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace GerenciadorDeViagem.WEB.Models.Api
{

    public class AdministradorApi : IAdministrador
    {
        private readonly IOptions<AdministradorEndPoint> _administradorEndPoint;
        private readonly IApiMetodos _apiCliente;

        public AdministradorApi(IOptions<AdministradorEndPoint> administradorEndPoint, IApiMetodos apiCliente)
        {
            _administradorEndPoint = administradorEndPoint;
            _apiCliente = apiCliente;
        }
        public async Task<bool> CadatroUsuario(Usuario usuario)
        {
            var endpoint = _administradorEndPoint.Value.CadatroUsuario;

            var dadosUsuarioJson = JsonSerializer.Serialize(usuario);

            var dadosUsuario = new StringContent(dadosUsuarioJson, Encoding.UTF8, "application/json");

            var resposta = await _apiCliente.Enviar(endpoint, dadosUsuario);


            if (resposta == null)
                return false;


            var dadosRetorno =  JsonSerializer.Deserialize<bool>(resposta.ToString()!);


            return dadosRetorno;

        }

        public async Task<Usuario> ConsultarUsuario(int matricula)
        {
            var endpoint = _administradorEndPoint.Value.ConsultarUsuario + matricula;

            var resposta = await _apiCliente.Obter(endpoint);

            if (resposta == null)
                return null!;


            var dadosRetorno = JsonSerializer.Deserialize<Usuario>(resposta.ToString()!);

            return dadosRetorno!;
        }

        public Task DeletaUsuario(int matricula)
        {
            throw new NotImplementedException();
        }
    }
}
