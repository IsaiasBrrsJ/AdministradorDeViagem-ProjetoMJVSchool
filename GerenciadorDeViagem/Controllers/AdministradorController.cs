using GerenciadorDeViagem.Data;
using GerenciadorDeViagem.Data.Dal.Interfaces;
using GerenciadorDeViagem.Data.Dao;
using GerenciadorDeViagem.Model;
using GerenciadorDeViagem.Model.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GerenciadorDeViagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorDal _administradorDal;
        public AdministradorController([FromServices] IAdministradorDal administradorDal)
        {
            _administradorDal = administradorDal;
        }
        
        [HttpPost("CadastroUsuario")]
        public async Task<IActionResult> CadastroUsuario([Bind(nameof(usuario.Matricula), nameof(usuario.NomeCompleto),
        nameof(usuario.Email), nameof(usuario.TipoDeUsuario))] Usuario usuario)
        {
           
            var cadastrouNoSistema = await _administradorDal.CriaUsuarioNoSistema(usuario);

            if (cadastrouNoSistema is false)
                return BadRequest(new {BadRequest = "Erro ao cadastrar usuário"});

            var uri = new Uri($"/api/Administrador/CadastroUsuario/{usuario.Matricula}", UriKind.Relative);

            return Created(uri, new { Success = "Cadastro realizado com sucesso" });
       
        }


        [HttpGet("ConsultarUsuario/{matricula}")]
        public async Task<IActionResult> ConsultarUsuario([FromRoute] int matricula)
        {
           var user =  await _administradorDal.ConsultarUsuarioNoSistema(matricula);

            if (user is null)
                return NotFound();


            return Ok(user);
        }

        [HttpDelete("DeletaUsuario/{matricula}")]
        public async Task<IActionResult> DeletaUsuario([FromRoute] int matricula)
        {
            var usuarioDeletado = await _administradorDal.DeletaUsuarioNoSistema(matricula);

            if (usuarioDeletado is false)
                return BadRequest();

            return NoContent();
        }

        [HttpPut("AlterarUsuario/{matricula}")]
        public async Task<IActionResult> AtualizaUsuario()
        {
            


             return Ok();
        }

    }
}
