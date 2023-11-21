using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using GerenciadorDeViagem.WEB.Models.EndPoints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace GerenciadorDeViagem.WEB.Models.Api
{
    public class LoginApi : ILoginApi
    {
        private readonly IOptions<LoginEndPoint> _loginEndPoint;
        private readonly IApiMetodos _apiCliente;
        
        public LoginApi(IOptions<LoginEndPoint> loginEndPoint, IApiMetodos apiCliente)
        {
            _loginEndPoint = loginEndPoint;
            _apiCliente = apiCliente;
        }

        public async Task<UsuarioLogin> Login(UsuarioLogin usuarioLogin)
        {
            var usuarioDadosLogin = new
            {
                Matricula = usuarioLogin.Matricula,
                Senha = usuarioLogin.Senha
            };

            var jsonDadosLogin = JsonSerializer.Serialize(usuarioDadosLogin);

            StringContent dados = new StringContent(jsonDadosLogin, Encoding.UTF8, "application/json");

            var endpoint = _loginEndPoint.Value.LoginUrl;
            var resposta = await _apiCliente.Enviar(endpoint, dados);

            if (resposta == null)
                return null!;


            var dadosRetorno = JsonSerializer.Deserialize<UsuarioLogin>(resposta.ToString()!);

            return dadosRetorno!;
        }
        public async Task<bool> AlterarSenha(int matricula, string senha, string novaSenha)
        {
            var usuarioLogin = new
            {
                senha = senha,
                novaSenha = novaSenha
            };

            var jsonDadosLogin = JsonSerializer.Serialize(usuarioLogin);

            StringContent dados = new StringContent(jsonDadosLogin, Encoding.UTF8, "application/json");

            var endpoint = _loginEndPoint.Value.LoginAlteraSenhaUrl+matricula;

            var resposta = await _apiCliente.Atualizar(endpoint, dados);

            
            return resposta;
        }

     
    }
}
