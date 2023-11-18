using GerenciadorDeViagem.WEB.Models;
using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeViagem.WEB.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginApi _loginApi = default!;
        public LoginController(ILoginApi loginApi)
        {
            _loginApi = loginApi;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Matricula, Senha")] UsuarioLogin usuarioLogin)
        {
           var usuario =  await _loginApi.Login(usuarioLogin);
           
            return RedirectToAction("Index", "Home", usuario);
        }
    }
}
