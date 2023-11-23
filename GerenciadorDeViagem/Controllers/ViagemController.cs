using GerenciadorDeViagem.Data.Dal.Interfaces;
using GerenciadorDeViagem.Data.Dao;
using GerenciadorDeViagem.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GerenciadorDeViagem.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ViagemController : ControllerBase
    {
        private readonly IViagemDal _viagemDal;

        public ViagemController([FromServices] IViagemDal viagemDal)
        {
            _viagemDal = viagemDal;
        }



        [HttpGet("ConsultarViagem/{matricula}")]
        public async Task<IActionResult> ConsultaViagem([FromRoute] int matricula)
        {

            var viagemSituacao = await _viagemDal.ConsultaViagem(matricula);

            if (viagemSituacao is null)
                return NotFound();

           var viagemJson = JsonSerializer.Serialize(viagemSituacao);

            return Ok(viagemJson);
        }
        [HttpGet("BuscarViagemPorId/{Id}")]
        public async Task<IActionResult> ObterViagemPorId([FromRoute] int Id)
        {
            var viagemPorId = await _viagemDal.ObterViagemPorId(Id);

            if (viagemPorId is null)
                return NotFound();

            var viagemJson = JsonSerializer.Serialize(viagemPorId);

            return Ok(viagemJson);
        }
        [HttpPost("CadastrarViagem")]
        public async Task<IActionResult> MarcarViagem([Bind(nameof(viagem.Destino), nameof(viagem.DataIda),
            nameof(viagem.DataVolta), nameof(viagem.TipoTransporte),
            nameof(viagem.MatriculaSolicitante), nameof(viagem.MatriculaSolicitante))]Viagem viagem)
        {
            var viagemCadastrada = await _viagemDal.CadastrarViagem(viagem);

            if (viagemCadastrada is false)
                return BadRequest();


            await GerenciadorDeViagemLogs.LogsAcaosAdministrador.GravaLogCadastroViagemNoSistema(viagem.MatriculaSolicitante);
            
            return Ok(viagemCadastrada);
        }
        [HttpPatch("CancelaViagem/{Id}")]
        public async Task<IActionResult> CancelaViagem([FromRoute] int Id)
        {
            var viagemCancelada = await _viagemDal.CancelarViagem(Id);

            if(viagemCancelada is false)
                return NotFound();


            await GerenciadorDeViagemLogs.LogsAcaosAdministrador.GravaLogViagemCanceladaNoSistema(Id);

            return Ok(viagemCancelada);
        }
 
        [HttpPatch("Aprovar/{Id}")]
        public async Task<IActionResult> AprovarViagem([FromRoute] int Id)
        {
            var viagemAprovada = await _viagemDal.AprovarViagem(Id);

            if (viagemAprovada is false)
                return NotFound();

            await GerenciadorDeViagemLogs.LogsAcaosAdministrador.GravaLogAprovacaoViagemNoSistema(Id);


            return Ok(viagemAprovada);
        }
    }
}
