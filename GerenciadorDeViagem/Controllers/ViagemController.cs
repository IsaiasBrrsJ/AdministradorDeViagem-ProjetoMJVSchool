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

        [HttpPost("CadastrarViagem")]
        public async Task<IActionResult> MarcarViagem([Bind(nameof(viagem.Destino), nameof(viagem.DataIda),
            nameof(viagem.DataVolta), nameof(viagem.TipoTransporte),
            nameof(viagem.MatriculaSolicitante), nameof(viagem.MatriculaSolicitante))]Viagem viagem)
        {
            var viagemCadastrada = await _viagemDal.CadastrarViagem(viagem);
            
            if (viagemCadastrada is false)
                return BadRequest();


            return Ok();
        }

        [HttpGet("ConsultarViagem/{matricula}")]
        public async Task<IActionResult> ConsultaViagem([FromRoute] int matricula)
        {

            var viagemSituacao = await _viagemDal.ConsultaViagem(matricula);

            if (viagemSituacao is null)
                return NotFound();

            return Ok(viagemSituacao);
        }
        [HttpPatch("CancelaViagem/{Id}")]
        public async Task<IActionResult> CancelaViagem([FromRoute] int Id)
        {
            var viagemCancelada = await _viagemDal.CancelarViagem(Id);

            if(viagemCancelada is false)
                return NotFound();

            return Ok(new {OK = "Viagem cancelada com sucesso"});
        }

        [HttpPatch("Aprovar/{Id}")]
        public async Task<IActionResult> AprovarViagem([FromRoute] int Id)
        {
            var viagemAprovada = await _viagemDal.CancelarViagem(Id);

            if (viagemAprovada is false)
                return NotFound();

            return Ok(new { OK = "Viagem Aprovada com sucesso" });
        }
    }
}
