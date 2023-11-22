using Microsoft.AspNetCore.Mvc;
using GerenciadorDeViagem.WEB.Models;
using GerenciadorDeViagem.WEB.Models.Api.Interfaces;

namespace GerenciadorDeViagem.WEB.Controllers
{
    public class ViagensController : Controller
    {
        private readonly IUsuario _usuario;

        public ViagensController(IUsuario usuario)
        {
           _usuario = usuario;
        }

        public async Task<IActionResult> PaginaAdministrador(int matricula)
        {
            var usuarioViagem = await _usuario.ObterViagemPorMatriculaListAsync(matricula);

            foreach(var user in usuarioViagem)
            {
                user.MatriculaUserLogado =  matricula;
            }

            return View(usuarioViagem);
        }

        public async Task<IActionResult> PaginaUsuario(int matricula)
        {
            var use = await _usuario.ObterViagemPorMatriculaListAsync(matricula);

            return View(use);
        }

        public async Task<IActionResult> Details(int? id)
        {
           
            return View();
        }

      
        public async Task<IActionResult> MarcarViagem(int matriculaUserLogado)
        {

            return View(new Viagem{ MatriculaUserLogado = matriculaUserLogado });
        }

        [HttpPost, ActionName("MarcarViagemConfirma")]
        public async Task<IActionResult> MarcarViagemConfirma([Bind("Destino,DataIda,DataVolta,TipoTransporte,MatriculaAprovador,MatriculaSolicitante")] Viagem viagem)
        {
            if (viagem.MatriculaAprovador.ToString().Length < 6)
            {
                ViewBag.MatriculaErrada = "Matricula deve ter 6 caracteres";
                return View(viagem);
            }
            else if(viagem.MatriculaSolicitante == viagem.MatriculaAprovador)
            {
                ViewBag.MatriculaErrada = "Matricula do aprovador e solicitante não podem ser iguais";

                return View(new Viagem { MatriculaUserLogado = viagem.MatriculaSolicitante });
            }
            
            if (viagem == null)
                return View();


            await _usuario.CadastrarViagem(viagem);
            
            return View(viagem);
        }

        public async Task<IActionResult> AprovarViagem(int id)
        {
            var usuarioViagem = await _usuario.ObterViagemPorIdAsync(id);

            return View(usuarioViagem);
        }


        [HttpPost, ActionName("AprovarViagemConfirma")]
        public async Task<IActionResult> AprovarViagemConfirma(int id, int matriculaAprovador)
        {
            var viagemAprovada = await _usuario.AprovarViagemAsync(id);

            return RedirectToAction("PaginaAdministrador", new { matricula = matriculaAprovador });
        }
        public async Task<IActionResult> CancelarViagem(int id)
        {
            var usuarioViagem =await _usuario.ObterViagemPorIdAsync(id);

            return View(usuarioViagem);
        }


        [HttpPost, ActionName("CancelarViagemConfirma")]
        public async Task<IActionResult> CancelarViagemConfirma(int id, int matriculaAprovador)
        {
            var viagemCancelada = await _usuario.CancelarViagemAsync(id);
            
            return RedirectToAction("PaginaAdministrador", new {matricula = matriculaAprovador });
        }

    }
}
