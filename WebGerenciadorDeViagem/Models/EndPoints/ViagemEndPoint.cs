namespace GerenciadorDeViagem.WEB.Models.EndPoints
{
    public class ViagemEndPoint
    {
        public string ConsultarViagemUrl { get; set; } = default!;
        public string ConsultarViagemPorIdUrl { get; set; } = default!; 

        public string CancelarViagemUrl { get; set; } = default!;   

        public string AprovarViagemUrl { get; set; } = default!;

        public string CadastrarViagemUrl { get; set; } = default!;
    }
}
