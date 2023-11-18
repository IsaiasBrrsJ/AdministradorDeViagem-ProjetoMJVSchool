using GerenciadorDeViagem.Model.Enum;
using System.Text.RegularExpressions;

namespace GerenciadorDeViagem.Model
{
    public class Usuario
    {
        public Usuario(int matricula, string nomeCompleto, string email, TipoDeUsuario tipoDeUsuario)
        {
            Matricula = matricula;
            NomeCompleto = nomeCompleto;
            Email = email;
            TipoDeUsuario = tipoDeUsuario;
            GerarSenha();
        }

        public int Id { get; private set; }
        public int Matricula { get; private set; }
        public string Email { get; private set; }
        public string NomeCompleto { get; private set; } = String.Empty;
        public TipoDeUsuario TipoDeUsuario { get; private set; }
        public string Senha { get;  private set; } = String.Empty;

        private void GerarSenha()
        {
            var senha = String.Empty;
            var random = new Random();

            var caracteresEspeciais = new[] { '@', '-', ')', '(', '%', '#', '*', '#', '_' };
            var alfabeto = new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };

            var letraMinuscula = alfabeto[random.Next(0, alfabeto.Length)].ToString().ToLower();
            var numero = random.Next(0, 9).ToString();
            var letraMaiuscula = alfabeto[random.Next(0, alfabeto.Length)];
            var caracteresEspecial = caracteresEspeciais[random.Next(0, caracteresEspeciais.Length)];

            var geraSenha = new object[] { numero, letraMinuscula, letraMaiuscula, caracteresEspecial };

            int tamanhoMaximoDaSenha = 0;

            while (tamanhoMaximoDaSenha < 12)
            {
                senha += geraSenha[random.Next(0, geraSenha.Length)];
                tamanhoMaximoDaSenha++;
            }

            Senha = senha;

        }

    }


}
