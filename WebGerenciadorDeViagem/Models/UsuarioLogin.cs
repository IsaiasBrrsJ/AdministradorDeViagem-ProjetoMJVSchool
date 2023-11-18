using System.Text.Json.Serialization;

namespace GerenciadorDeViagem.WEB.Models
{
    public class UsuarioLogin
    {
        public int Matricula { get; set; }

        public string Senha { get; set; } = String.Empty;

        public TipoDeUsuario TipoUsuario { get; set; }
    }
}
