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
        private readonly ILoginApi _loginApi;
        private readonly IApiMetodos _apiCliente;
        
        public LoginApi( IOptions<LoginEndPoint> loginEndPoint, ILoginApi loginApi, IApiMetodos apiCliente)
        {
            _loginEndPoint = loginEndPoint;
            _loginApi = loginApi;
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


             await _apiCliente.Enviar(_loginEndPoint.Value.ToString()!, dados);

            return null!;
        }
        public Task<UsuarioLogin> AlterarSenha(int matricula, string senha)
        {
            throw new NotImplementedException();
        }

     
    }
}
