using GerenciadorDeViagem.Data.Dal.Interfaces;
using GerenciadorDeViagem.Data.Interfaces;
using GerenciadorDeViagem.Model;
using GerenciadorDeViagem.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.RegularExpressions;

namespace GerenciadorDeViagem.Data.Dao
{
    public class AdministradorDal : IAdministradorDal
    {
        private readonly IBanco _connection;
        private readonly SqlCommand _command;
        
        public record DadosUsuario(int Matricula, string NomeCompleto, string Email, TipoDeUsuario TipoDeUsuario);
        public AdministradorDal([FromServices] IBanco banco)
        {
            
            _connection = banco;
            _command = new SqlCommand();
            _command.CommandType =CommandType.Text;
        }
     
        public async Task<bool> CriaUsuarioNoSistema(Usuario usuario)
        {

           
            try
            {
                if (!validaCamposUsuario(usuario))
                    return false;



                 var conexaoComBanco = _connection.AbrirConexao();
                _command.Connection = conexaoComBanco;
                _command.CommandText = @"INSERT INTO dbo.Usuario 
                                       (Matricula, NomeCompleto,Email, TipoUsuario, Senha)
                                       VALUES
                                       (@Matricula, @NomeCompleto,@Email, @TipoUsuario, @Senha)";


                _command.Parameters.AddWithValue("@Matricula",SqlDbType.Int).Value = usuario.Matricula;
                _command.Parameters.AddWithValue("@NomeCompleto",SqlDbType.VarChar).Value = usuario.NomeCompleto;
                _command.Parameters.AddWithValue("@Email",SqlDbType.VarChar).Value = usuario.Email.ToLower();
                _command.Parameters.AddWithValue("@TipoUsuario",SqlDbType.Int).Value = usuario.TipoDeUsuario;
                _command.Parameters.AddWithValue("@Senha",SqlDbType.VarChar).Value = usuario.Senha;


                var linhasAfetadas = await _command.ExecuteNonQueryAsync();

                if (linhasAfetadas <= 0)
                    return false;

                return true;

            }
            catch (SqlException)
            {
                //caso tente cadastrar com alguma matricula ou email já cadastrado ele vai estourar erro, pois esses campos tem o tipo
                //de dados com unique;


                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _connection.FecharConexao();
                _command.Dispose();
            }
            
        }
        public async Task<DadosUsuario> ConsultarUsuarioNoSistema(int matricula)
        {
            try
            {

                _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"SELECT Matricula,NomeCompleto, Email, TipoUsuario FROM dbo.usuario 
                                         WHERE Matricula = @Matricula";
                                        

                _command.Parameters.AddWithValue("@Matricula", SqlDbType.Int).SqlValue = matricula;
                

                var dadosDoBanco = await _command.ExecuteReaderAsync();

                DadosUsuario usuario = null!;

                while (dadosDoBanco.Read())
                {

                    usuario = new
                    DadosUsuario((int)dadosDoBanco["Matricula"], (string)dadosDoBanco["NomeCompleto"],
                    (string)dadosDoBanco["Email"], (TipoDeUsuario)((int)(dadosDoBanco["TipoUsuario"])));
                }


                return usuario;
            }
            catch (SqlException)
            {
                return null!;
            }
            catch (Exception)
            {
                return null!;
            }
            finally
            {
                _connection.FecharConexao();
                _command.Dispose();
            }
        }

        public async Task<bool> DeletaUsuarioNoSistema(int matricula)
        {
            try
            {

                _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"DELETE FROM dbo.usuario 
                                         WHERE Matricula = @Matricula";

                _command.Parameters.AddWithValue("@Matricula", SqlDbType.Int).SqlValue = matricula;

                 var linhasAfetadas = await _command.ExecuteNonQueryAsync();

                if(linhasAfetadas <= 0)
                 return false;

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _connection.FecharConexao();
                _command.Dispose();
            }
        }

        public async Task<bool> AtualizaUsuarioNoSistema() 
        {
            return true;
        }


        private  bool validaCamposUsuario(Usuario usuario)
        {

            var regexValidaEmail = @"^[A-Za-z]{1,}[a-zA-Z0-9._%+-]+@(email\.com|empresa\.com)$";
            var regexValidaNome = @"^[A-Z][a-z]{2,}( [a-zA-Z]+)*$";

            if ((String.IsNullOrWhiteSpace(usuario.NomeCompleto) || !Regex.IsMatch(usuario.NomeCompleto, regexValidaNome)) || (usuario.TipoDeUsuario != TipoDeUsuario.Usuario && usuario.TipoDeUsuario != TipoDeUsuario.Administrador))
                return false;

            var rege = !Regex.IsMatch(usuario.Email, regexValidaEmail);
            var ass = !(usuario.Matricula.ToString().Length == 6);

            if (!Regex.IsMatch(usuario.Email, regexValidaEmail) || !(usuario.Matricula.ToString().Length == 6))
                return false;



            return true;
        }
    }
}
