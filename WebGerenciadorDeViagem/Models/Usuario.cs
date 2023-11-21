using GerenciadorDeViagem.WEB.Models.Enum;

namespace GerenciadorDeViagem.WEB.Models
{
    public class Usuario
    {
       
        public int Matricula { get; private set; }
        public string NomeCompleto { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public TipoDeUsuario TipoDeUsuario { get; private set; }
    }
}
