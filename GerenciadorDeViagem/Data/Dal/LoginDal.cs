using GerenciadorDeViagem.Data.Dal.Interfaces;
using GerenciadorDeViagem.Data.Interfaces;
using GerenciadorDeViagem.Model;
using GerenciadorDeViagem.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace GerenciadorDeViagem.Data.Dao
{
    public class LoginDal : ILoginDal
    {
        private readonly IBanco _connection;
        private readonly SqlCommand _command;
    
        public LoginDal([FromServices] IBanco connection)
        {
            _connection = connection;
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

        public async Task<bool> AlterarSenha(int matricula, string senha, string novaSenha)
        {
            try
            {
                _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"UPDATE dbo.usuario
                                         SET Senha = @NovaSenha
                                         WHERE Matricula = @Matricula and Senha=@senha";

                _command.Parameters.AddWithValue("@Matricula", SqlDbType.Int).SqlValue = matricula;
                _command.Parameters.AddWithValue("@Senha", SqlDbType.Int).SqlValue = senha;
                _command.Parameters.AddWithValue("@NovaSenha", SqlDbType.Int).SqlValue = novaSenha;

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
    }
}
