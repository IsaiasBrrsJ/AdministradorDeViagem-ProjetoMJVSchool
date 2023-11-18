using GerenciadorDeViagem.Data;
using GerenciadorDeViagem.Data.Dal.Interfaces;
using GerenciadorDeViagem.Data.Dao;
using GerenciadorDeViagem.Model;
using GerenciadorDeViagem.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace GerenciadorDeViagem.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private record UsuarioLogado(int matricula, TipoDeUsuario tipoUsuario);
        private readonly ILoginDal _loginDal;
       
        public LoginController([FromServices] ILoginDal loginDal)
        {
            _loginDal = loginDal;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([Bind(nameof(usuarioLogin.Matricula), nameof(usuarioLogin.Senha))] UsuarioLogin usuarioLogin)
        {
            var usuarioBanco = await _loginDal.ValidaCredenciais(usuarioLogin);

            if ( (usuarioLogin.Matricula != usuarioBanco.Matricula || usuarioLogin.Senha != usuarioBanco.Senha ) || usuarioBanco is null)
                return NotFound(new {NotFound = "Erro ao  de login, valide seus dados ou tente novamente"});

            var usuarioLogadoJson = JsonSerializer.Serialize(new UsuarioLogado(usuarioBanco.Matricula, usuarioBanco.TipoUsuario));
            
            return Ok(usuarioLogadoJson);

        }


        [HttpPatch("AtualizarSenha/{matricula}")]
        public async Task<IActionResult> AlterarSenha([FromRoute] int matricula, [FromBody] string senha)
        {
            var senhaAlterada = await _loginDal.AlterarSenha(matricula, senha);

            if (senhaAlterada is false)
                return BadRequest();


            return Ok(new { Ok = "Senha alterada com sucesso" });
        }
    }
}
