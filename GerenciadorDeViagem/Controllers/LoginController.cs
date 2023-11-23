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
        public record UsuarioLogado(int Matricula,string Senha, TipoDeUsuario TipoUsuario);
        public record AlterarSenhaUsuario(string senha, string novaSenha);

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

            var usuarioLogado = new UsuarioLogado(usuarioBanco.Matricula, String.Empty, usuarioBanco.TipoUsuario);

           await GerenciadorDeViagemLogs.LogLogin.GravaLogLogin(new GerenciadorDeViagemLogs.LogLogin.UsuarioLogado(usuarioBanco.Matricula, usuarioBanco.TipoUsuario.ToString()));

            var json = JsonSerializer.Serialize(usuarioLogado);
            
            
            return Ok(json);

        }


        [HttpPatch("AtualizarSenha/{matricula}")]
        public async Task<IActionResult> AlterarSenha([FromRoute] int matricula,[Bind("senha, novaSenha")] AlterarSenhaUsuario alterarSenhaUsuario)
        {
            var senhaAlterada = await _loginDal.AlterarSenha(matricula, alterarSenhaUsuario.senha, alterarSenhaUsuario.novaSenha);

            if (senhaAlterada is false)
                return BadRequest();

            await GerenciadorDeViagemLogs.LogLogin.GravaLogAlteracaoDeSenha(matricula);

            return Ok(new { Ok = "Senha alterada com sucesso" });
        }
    }
}
