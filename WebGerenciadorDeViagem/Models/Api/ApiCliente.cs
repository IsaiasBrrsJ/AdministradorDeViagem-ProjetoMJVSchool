using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using System.Text.Json;

namespace GerenciadorDeViagem.WEB.Models.Api
{
    public class ApiCliente : IApiMetodos
    {
  
     
        private HttpClient _httpClient = default!;
        public ApiCliente()
        {
            _httpClient = new HttpClient();
        }
        public async Task<object> Obter(string endpoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return null!;


            var dadosRetorno = response.Content.ReadAsStringAsync().Result;

            return dadosRetorno;
        }

        public async Task<object> Enviar(string endpoint, StringContent dados)
        {
            
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, dados);

            if (!response.IsSuccessStatusCode)
                return null!;

            var dadosRetorno = response.Content.ReadAsStringAsync().Result;

            return dadosRetorno;
        }

        public async Task<bool> Atualizar(string endpoint, StringContent dados)
        {
            HttpResponseMessage response = await _httpClient.PatchAsync(endpoint, dados);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
          
        }
        public async Task Deletar(string endpoint)
        {
            throw new NotImplementedException();
        }

      
    }
}
