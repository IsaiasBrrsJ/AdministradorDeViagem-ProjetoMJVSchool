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
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                    return null!;


                var dadosRetorno = response.Content.ReadAsStringAsync().Result;

                return dadosRetorno;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<object> Enviar(string endpoint, StringContent dados)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, dados);

                if (!response.IsSuccessStatusCode)
                    return null!;

                var dadosRetorno = response.Content.ReadAsStringAsync().Result;

                return dadosRetorno;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<bool> Atualizar(string endpoint, StringContent dados)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PatchAsync(endpoint, dados);

                if (!response.IsSuccessStatusCode)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Deletar(string endpoint)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                    return false;

                var dadosRetorno =Convert.ToBoolean(response.Content.ReadAsStringAsync().Result);

                return dadosRetorno;
            }
            catch
            {
                return false;
            }
        }

      
    }
}
