using GerenciadorDeViagem.WEB.Models.EndPoints;
using GerenciadorDeViagem.WEB.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GerenciadorDeViagem.WEB.Models
{
    public class Viagem
    {
        public int Id { get;  set; }

        [Required(ErrorMessage = "Por favor, preencha o seu destino")]
        public string Destino { get;  set; } = String.Empty;

        [Required(ErrorMessage = "Por favor, preencha a data de partida")]
        public DateTime DataIda { get;  set; }

        [Required(ErrorMessage = "Por favor, preencha a data da volta")]
        public DateTime DataVolta { get;  set; }
        public DateTime DataDaSolicitacao { get; set; }

        [Required(ErrorMessage = "Por favor, preencha o tipo de transporte")]
        public TipoTransporte TipoTransporte { get;  set; }
        public StatusViagem StatusViagem { get; set; }

        [Required(ErrorMessage = "Por favor, preencha a matricula do aprovador")]
        public int MatriculaAprovador { get;  set; }
        public int MatriculaSolicitante { get;  set; }
        public object DataAprovacaoRecusa { get; set; } = default!;
        
        [JsonIgnore]
        public int MatriculaUserLogado { get; set; }
    }
}
