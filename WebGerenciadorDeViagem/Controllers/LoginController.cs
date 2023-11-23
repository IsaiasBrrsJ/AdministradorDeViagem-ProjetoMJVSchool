using GerenciadorDeViagem.WEB.Models;
using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using GerenciadorDeViagem.WEB.Models.Enum;
using Microsoft.AspNetCore.Authentication;
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

        public async Task<IActionResult> Login(StatusLogin statusLoing = StatusLogin.NaoFezLogin)
        {
            ViewBag.LoginUsuario = statusLoing;
            return View();
        }

        public async Task<IActionResult> MudarSenha(StatusLogin statusLoing = StatusLogin.NaoFezLogin)
        {
            ViewBag.LoginUsuario = statusLoing;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Matricula, Senha")] UsuarioLogin usuarioLogin)
        {
         

            var usuario = await _loginApi.Login(usuarioLogin);

            if(usuario is null)
                return RedirectToAction("Login", "Login", new { statusLoing  =StatusLogin.LoginErro });


            if (usuario.TipoUsuario == TipoDeUsuario.Administrador)
                return RedirectToAction("PaginaAdministrador", "Viagens", new { matricula = usuario.Matricula, tipoUsuario = TipoDeUsuario.Administrador });
            
            else if (usuario.TipoUsuario == TipoDeUsuario.Usuario)
                return RedirectToAction("PaginaUsuario", "Viagens", new { matricula = usuario.Matricula, tipoUsuario = TipoDeUsuario.Usuario });
           
            else
            return RedirectToAction("Index", "Home", usuario);
        }

       

        [HttpPost]
        public async Task<IActionResult> MudarSenha([Bind("Matricula, Senha, NovaSenha")] AlterarSenhaUsuario usuarioLogin)
        {
            var usuario = await _loginApi.AlterarSenha(usuarioLogin.Matricula, usuarioLogin.Senha, usuarioLogin.NovaSenha);


            if (usuario is false)
                return RedirectToAction("MudarSenha", "Login", new { statusLoing = StatusLogin.LoginErro });


            return RedirectToAction("MudarSenha", "Login", new { statusLoing = StatusLogin.SenhaAlteradaComSucesso});
        }

     
        public async Task<IActionResult> Sair()
        {
            return RedirectToAction("Login", "Login");
        }
    }
}
