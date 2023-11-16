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
        private readonly ViagemDal _viagemDal;

        public ViagemController()
        {
            _viagemDal = new ViagemDal();
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


            var viagemJson =  JsonSerializer.Serialize(viagemSituacao);

            return Ok(viagemJson);
        }
    }
}
