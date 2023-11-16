using GerenciadorDeViagem.Data;
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
        private readonly AdministradorDal _administradorDal;
        public AdministradorController()
        {
           _administradorDal = new AdministradorDal();
        }
        
        [HttpPost("CadastroUsuario")]
        public async Task<IActionResult> CadastroUsuario([Bind(nameof(usuario.Matricula), nameof(usuario.NomeCompleto),
        nameof(usuario.Email), nameof(usuario.TipoDeUsuario))] Usuario usuario)
        {
           
            var cadastroNoSistema = await _administradorDal.CriaUsuarioNoSistema(usuario);

            if (cadastroNoSistema is false)
                return BadRequest(new {BadRequest = "Erro ao cadastrar usuário"});

            return Ok(new { OK = "Cadastro realizado com sucesso" });
        }


        [HttpGet("ConsultarUsuario/{matricula}")]
        public async Task<IActionResult> ConsultarUsuario([FromRoute] int matricula)
        {
           var user =  await _administradorDal.ConsultarUsuarioNoSistema(matricula);

            var userJson = JsonSerializer.Serialize(user);

            return Ok(userJson);
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
