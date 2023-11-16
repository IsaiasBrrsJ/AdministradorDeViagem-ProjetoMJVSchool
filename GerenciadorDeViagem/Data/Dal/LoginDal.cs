using GerenciadorDeViagem.Model;
using GerenciadorDeViagem.Model.Enum;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GerenciadorDeViagem.Data.Dao
{
    public class LoginDal
    {
        private readonly Banco _connection;
        private readonly SqlCommand _command;
        public LoginDal()
        {
            _connection = new Banco();
            _command = new SqlCommand();
            _command.CommandType = CommandType.Text;
        }

        public async Task<UsuarioLogin> ValidaCredenciais(UsuarioLogin usuarioLogin)
        {
            try
            {
                _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"SELECT Matricula, Senha, TipoUsuario FROM dbo.usuario 
                                         WHERE Matricula = @Matricula 
                                         and Senha = @Senha";

                _command.Parameters.AddWithValue("@Matricula", SqlDbType.Int).SqlValue = usuarioLogin.Matricula;
                _command.Parameters.AddWithValue("@Senha", SqlDbType.VarChar).SqlValue = usuarioLogin.Senha;
               
                var dadosDoBanco = await _command.ExecuteReaderAsync();

                var usuarioLoginBanco = new UsuarioLogin();

                while (dadosDoBanco.Read())
                {
                    usuarioLoginBanco.Matricula = (int)dadosDoBanco["Matricula"];
                    usuarioLoginBanco.Senha = (string)dadosDoBanco["Senha"];
                    usuarioLoginBanco.TipoUsuario = (TipoDeUsuario)((int)(dadosDoBanco["TipoUsuario"]));
                }


                return usuarioLoginBanco;
            }
            catch(SqlException)
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

        public async Task<bool> AlterarSenha(int matricula, string senha)
        {
            try
            {
                _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"UPDATE dbo.usuario
                                         SET Senha = @Senha
                                         WHERE Matricula = @Matricula";

                _command.Parameters.AddWithValue("@Matricula", SqlDbType.Int).SqlValue = matricula;
                _command.Parameters.AddWithValue("@Senha", SqlDbType.Int).SqlValue = senha;


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
    }
}
