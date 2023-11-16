using GerenciadorDeViagem.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace GerenciadorDeViagem.Model
{
    public class UsuarioLogin
    { 
        public int Matricula { get; set; }
       
        public string Senha { get;  set; } = String.Empty;
        
        [JsonIgnore]
        public TipoDeUsuario TipoUsuario { get;  set; }    
    }
}
