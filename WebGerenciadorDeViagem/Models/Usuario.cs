using GerenciadorDeViagem.WEB.Models.Enum;

namespace GerenciadorDeViagem.WEB.Models
{
    public class Usuario
    {
        public int Id { get;  set; }
        public int Matricula { get;  set; }
        public string NomeCompleto { get;  set; } = default!;
        public string Email { get;  set; } = default!;
        public TipoDeUsuario TipoDeUsuario { get;  set; }
    }
}
