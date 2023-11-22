using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeViagem.WEB.Data;
using GerenciadorDeViagem.WEB.Models;
using GerenciadorDeViagem.WEB.Models.Api.Interfaces;
using GerenciadorDeViagem.WEB.Models.Enum;

namespace GerenciadorDeViagem.WEB.Controllers
{
    public class UsuarioAdminController : Controller
    {
        private readonly IAdministrador _administrador;

        public UsuarioAdminController(IAdministrador administrador)
        {
            _administrador = administrador;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index(UsuarioEncontrado usuarioEncontrado = UsuarioEncontrado.NaoBuscouAinda,Pagina pagina = Pagina.IndexConsultarUsuario, int matricula = 0)
        {
            ViewBag.AchouUsuario = usuarioEncontrado;
            ViewBag.pagina = pagina;
            ViewBag.matricula = matricula;


            return View(new Usuario());
        }
        
        
        public async Task<IActionResult> DetalhesUsuario(int matricula)
        {
            if(matricula.ToString().Length == 6)
            {
                var usuarioSistema = await _administrador.ConsultarUsuario(matricula);

                if (usuarioSistema is null)
                    return RedirectToAction("Index", "UsuarioAdmin", new { usuarioEncontrado = UsuarioEncontrado.UsuarioNaoEncontrado});
               


                return View(usuarioSistema);
            }


            return RedirectToAction("Index", "UsuarioAdmin", new { usuarioEncontrado = UsuarioEncontrado.MatriculaIncorreta});
        }


        public async Task<IActionResult> CadastrarUsuario(SituacaoCadastro statusCadastro = SituacaoCadastro.CadastroPendente, Pagina pagina = Pagina.IndexConsultarUsuario, int matricula = 0)
        {

             ViewBag.CadastrouComSucesso = statusCadastro;
             ViewBag.pagina = pagina;
             ViewBag.matricula = matricula; 

            return View(new Usuario());
        }
        public async Task<IActionResult> VoltarPagina(Pagina pagina = Pagina.IndexConsultarUsuario, int matricula = 0)
        {
            if (pagina == Pagina.IndexAdministrador)
                return RedirectToAction("PaginaAdministrador", "Viagens", new {matricula = matricula});


            return RedirectToAction("Index", "UsuarioAdmin");
        }
        [HttpPost, ActionName("CadastrarUsuarioConfirma")]
        public async Task<IActionResult> CadastrarUsuarioConfirma([Bind("Matricula,NomeCompleto,Email,TipoDeUsuario")] Usuario usuario)
        {
            if (usuario == null)
                return View(new Usuario());
            
            var situacaoCadastro = SituacaoCadastro.CadastroPendente;


           var statusCadastro = await _administrador.CadatroUsuario(usuario);

            if (statusCadastro)
                situacaoCadastro = SituacaoCadastro.CadastroRealizado;
            else
                situacaoCadastro = SituacaoCadastro.ErroNoCadastro;


           return RedirectToAction("CadastrarUsuario", "UsuarioAdmin", new { statusCadastro = situacaoCadastro });
        }

   
        public async Task<IActionResult> Edit(int? id)
        {

            return View(new Usuario());
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Matricula,NomeCompleto,Email,TipoDeUsuario")] Usuario usuario)
        {

            return View(new Usuario());
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            return View(new Usuario());
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            return View(new Usuario());
        }

    }
}
