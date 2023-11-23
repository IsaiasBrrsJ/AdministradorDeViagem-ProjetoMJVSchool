using Microsoft.AspNetCore.Mvc;
using GerenciadorDeViagem.WEB.Models;
using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using GerenciadorDeViagem.WEB.Models.Enum;

namespace GerenciadorDeViagem.WEB.Controllers
{
    public class ViagensController : Controller
    {
        private readonly IUsuario _usuario;

        public ViagensController(IUsuario usuario)
        {
           _usuario = usuario;
        }

        public async Task<IActionResult> PaginaAdministrador(int matricula, TipoDeUsuario tipoUsuario)
        {
            var usuarioViagem = await _usuario.ObterViagemPorMatriculaListAsync(matricula);

            ViewBag.Matricula = matricula;
            ViewBag.TipoUsuario = tipoUsuario;

            return View(usuarioViagem);
        }

        public async Task<IActionResult> PaginaUsuario(int matricula, TipoDeUsuario tipoUsuario)
        {
            var usuarioViagem = await _usuario.ObterViagemPorMatriculaListAsync(matricula); 

            ViewBag.Matricula = matricula;
            ViewBag.TipoUsuario = tipoUsuario;

            return View(usuarioViagem);
        }

       public async Task<IActionResult> VoltarPagina(int matricula, TipoDeUsuario tipoDeUsuario)
       {
            if(tipoDeUsuario == TipoDeUsuario.Administrador)
                return RedirectToAction("PaginaAdministrador", "Viagens", new {matricula, tipoUsuario = tipoDeUsuario });


            return RedirectToAction("PaginaUsuario", "Viagens", new {matricula, tipoUsuario = tipoDeUsuario });
        }

        public async Task<IActionResult> MarcarViagem(int matricula, TipoDeUsuario tipoDeUsuario, SituacaoCadastro situacaoCadastro)
        {
            ViewBag.MatriculaUserLogado = matricula;
            ViewBag.TipoUsuario = tipoDeUsuario;
            ViewBag.SituacaoCadastro = situacaoCadastro;

            return View(new Viagem());
        }

        [HttpPost, ActionName("MarcarViagemConfirma")]
        public async Task<IActionResult> MarcarViagemConfirma([Bind("Destino,DataIda,DataVolta,TipoTransporte,MatriculaAprovador,MatriculaSolicitante")] Viagem viagem, TipoDeUsuario tipoDeUsuario)
        {

            if(!ValidaViagem(viagem))
                return RedirectToAction("MarcarViagem", "Viagens", new { matricula = viagem.MatriculaSolicitante, tipoDeUsuario, situacaoCadastro = SituacaoCadastro.ErroNoCadastro });

            var cadastroRealizado =  await _usuario.CadastrarViagem(viagem);

            if(!cadastroRealizado)
                return RedirectToAction("MarcarViagem", "Viagens", new { matricula = viagem.MatriculaSolicitante, tipoDeUsuario, situacaoCadastro = SituacaoCadastro.ErroNoCadastro });


            return RedirectToAction("MarcarViagem", "Viagens", new { matricula = viagem.MatriculaSolicitante, tipoDeUsuario, situacaoCadastro = SituacaoCadastro.CadastroRealizado });
        }
        private bool ValidaViagem(Viagem viagem)
        {
            if (viagem == null)
                return false;

            else if (!(viagem.MatriculaAprovador.ToString().Length == 6)){
                return false;
            }
            else if (viagem.MatriculaSolicitante == viagem.MatriculaAprovador){ 
                return false;
            }
            else if (viagem.DataIda > viagem.DataVolta){ 
                return false;
            }

            return true;
        }

        public async Task<IActionResult> AprovarViagem(int id)
        {
            var usuarioViagem = await _usuario.ObterViagemPorIdAsync(id);

            ViewBag.MatriculaAprovador = usuarioViagem.MatriculaAprovador;
            
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

            ViewBag.MatriculaAprovador = usuarioViagem.MatriculaAprovador;

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
