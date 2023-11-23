using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeViagem.WEB.Models.Api.Interfaces
{
    public interface IAdministrador
    {
        Task<bool> CadatroUsuario(Usuario usuario);
        Task<Usuario> ConsultarUsuario(int matricula);
        Task<bool> DeletaUsuario(int matricula);

    }
}
