using GerenciadorDeViagem.Model;
using GerenciadorDeViagem.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GerenciadorDeViagem.Data.Dao
{
    public class AdministradorDal 
    {
        private readonly Banco _connection;
        private readonly SqlCommand _command;
        public record DadosUsuario(int matricula, string nomeCompleto, string email, TipoDeUsuario TipoDeUsuario);
        public AdministradorDal()
        {
            _connection = new Banco();
            _command = new SqlCommand();
            _command.CommandType =CommandType.Text;
        }
        public async Task<bool> CriaUsuarioNoSistema(Usuario usuario)
        {
            try
            {
               

                var conexaoComBanco = _connection.AbrirConexao();
                _command.Connection = conexaoComBanco;
                _command.CommandText = @"INSERT INTO dbo.Usuario 
                                       (Matricula, NomeCompleto,Email, TipoUsuario, Senha)
                                       VALUES
                                       (@Matricula, @NomeCompleto,@Email, @TipoUsuario, @Senha)";


                _command.Parameters.AddWithValue("@Matricula",SqlDbType.Int).Value = usuario.Matricula;
                _command.Parameters.AddWithValue("@NomeCompleto",SqlDbType.VarChar).Value = usuario.NomeCompleto;
                _command.Parameters.AddWithValue("@Email",SqlDbType.VarChar).Value = usuario.Email;
                _command.Parameters.AddWithValue("@TipoUsuario",SqlDbType.Int).Value = usuario.TipoDeUsuario;
                _command.Parameters.AddWithValue("@Senha",SqlDbType.VarChar).Value = usuario.Senha;


                await _command.ExecuteNonQueryAsync();
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
            
            return true;

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

                 await _command.ExecuteNonQueryAsync();

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
    }
}
