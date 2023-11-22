using GerenciadorDeViagem.Model.Enum;
using System.Text.Json.Serialization;

namespace GerenciadorDeViagem.Model
{
    public class Viagem
    {
        public Viagem( string destino, DateTime dataIda, DateTime dataVolta, TipoTransporte tipoTransporte, int matriculaAprovador, int matriculaSolicitante)
        {
            
            Destino = destino;
            DataIda = dataIda;
            DataVolta = dataVolta;
            TipoTransporte = tipoTransporte;
            MatriculaAprovador = matriculaAprovador;
            MatriculaSolicitante = matriculaSolicitante;
            StatusViagem = StatusViagem.Pendente;
            DataDaSolicitacao = DateTime.Now;
        }

        public int Id { get;  set; } 
        public string Destino { get; private set; } = String.Empty; 


        public DateTime DataIda { get; private set; }

        public DateTime DataVolta { get; private set; }
        public TipoTransporte TipoTransporte { get; private set; }
        public DateTime DataDaSolicitacao { get; private set; }

        [JsonIgnore]
        public DateTime DataAprovacaoRecusa { get; set; }

        [JsonIgnore]
        public StatusViagem StatusViagem { get; set; }

        public int MatriculaAprovador { get; private set; }
        public int MatriculaSolicitante { get; private set; }
      


    }
}
