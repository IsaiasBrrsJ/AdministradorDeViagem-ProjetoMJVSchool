using GerenciadorDeViagem.WEB.Models.Api.Interfaces;

namespace GerenciadorDeViagem.WEB.Models.Api
{
    public class ApiCliente : IApiMetodos
    {
        private readonly HttpClient _httpClient;
        private readonly IApiMetodos _apiMetodos;
        public ApiCliente(IApiMetodos apiMetodos)
        {
            _httpClient = new HttpClient();
            _apiMetodos = apiMetodos;

        }
        public async Task<object> Obter(string endpoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            return null!;
        }

        public async Task<object> Enviar(string endpoint, StringContent dados)
        {
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, dados);
            return null!;
        }
        public async  Task Atualizar(string endpoint)
        {
            throw new NotImplementedException();
        }
        public async Task Deletar(string endpoint)
        {
            throw new NotImplementedException();
        }

    


      
    }
}
