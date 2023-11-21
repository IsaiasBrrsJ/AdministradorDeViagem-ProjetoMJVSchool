using GerenciadorDeViagem.WEB.Models;
using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace GerenciadorDeViagem.WEB.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginApi _loginApi;
        public record AlterarSenhaUsuario(int Matricula, string Senha, string NovaSenha);

     
        public LoginController(ILoginApi loginApi)
        {
            _loginApi = loginApi;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> MudarSenha()
        {
            

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Matricula, Senha")] UsuarioLogin usuarioLogin)
        {
           var usuario =  await _loginApi.Login(usuarioLogin);

            if (usuario is null)
                return View();

            if (usuario.TipoUsuario == Models.Enum.TipoDeUsuario.Administrador)
                return RedirectToAction("PaginaAdministrador", "Viagens", new { matricula = usuario.Matricula});
            
            else if (usuario.TipoUsuario == Models.Enum.TipoDeUsuario.Usuario)
                return RedirectToAction("PaginaUsuario", "Viagens", new { matricula = usuario.Matricula});
           
            else
            return RedirectToAction("Index", "Home", usuario);
        }

       

        [HttpPost]
        public async Task<IActionResult> MudarSenha([Bind("Matricula, Senha, NovaSenha")] AlterarSenhaUsuario usuarioLogin)
        {
            var teste = await _loginApi.AlterarSenha(usuarioLogin.Matricula, usuarioLogin.Senha, usuarioLogin.NovaSenha);


            return View(teste);
        }


    }
}
