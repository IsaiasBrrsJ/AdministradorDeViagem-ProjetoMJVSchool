using GerenciadorDeViagem.WEB.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeViagem.WEB.Models
{
    public class Usuario
    {
        public int Id { get;  set; }

        [Required(ErrorMessage = "Por favor, preencha a matricula")]
        public int Matricula { get;  set; }

        [Required(ErrorMessage = "Por favor, preencha seu nome completo")]
        public string NomeCompleto { get;  set; } = default!;

        [Required(ErrorMessage = "Por favor, preencha seu email")]
        public string Email { get;  set; } = default!;

        [Required(ErrorMessage = "Por favor, informe o tipo de usuário")]
        public TipoDeUsuario TipoDeUsuario { get;  set; }
    }
}
