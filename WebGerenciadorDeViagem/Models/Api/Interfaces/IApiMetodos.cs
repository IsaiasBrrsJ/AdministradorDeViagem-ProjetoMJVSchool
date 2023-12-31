﻿namespace GerenciadorDeViagem.WEB.Models.Api.Interfaces
{
    public interface IApiMetodos
    {
        Task<Object> Obter(string endpoint);
        Task<Object> Enviar(string endpoint, StringContent dados);
        Task<bool> Deletar(string endpoint);
        Task<bool> Atualizar(string endpoint, StringContent dados);
    }
}
