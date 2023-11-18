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
    public class ViagemDal : IViagemDal
    {
        private readonly IBanco _connection;
        private readonly SqlCommand _command;
       
        public record ViagemConsulta(int Id, string destino, DateTime dataIda, DateTime dataVolta, DateTime dataSolicitacao,
                                     TipoTransporte tipoTransporte, StatusViagem statusViagem, Object DataAprovacaoRecusa);

        private List<ViagemConsulta> ViagemConsultas = default!;
        public ViagemDal([FromServices] IBanco connection)
        {

            _connection = connection;

            _command = new SqlCommand();
            _command.CommandType = CommandType.Text;
        }

        public async Task<bool> CadastrarViagem(Viagem viagem)
        {
            try
            {


               _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"INSERT INTO Viagem
                        (Destino, DataIda, DataVolta, DataSolicitacao, TipoTransporte,
                        StatusViagem, MatriculaAprovador, MatriculaSolicitante)
                        VALUES(@Destino, @DataIda,@DataVolta,@DataSolicitacao,@TipoTransporte, @StatusViagem,
                        (SELECT Matricula FROM Usuario WHERE Matricula = @MatriculaAprovador and TipoUsuario = @TipoUsuario),
                        (SELECT Matricula FROM Usuario WHERE Matricula = @MatriculaSolicitante));";


                _command.Parameters.AddWithValue("@Destino", SqlDbType.VarChar).Value = viagem.Destino;
                _command.Parameters.AddWithValue("@DataIda", SqlDbType.DateTime).Value = viagem.DataIda;
                _command.Parameters.AddWithValue("@DataVolta", SqlDbType.DateTime).Value = viagem.DataVolta;
                _command.Parameters.AddWithValue("@DataSolicitacao", SqlDbType.DateTime).Value = viagem.DataDaSolicitacao;
                _command.Parameters.AddWithValue("@TipoTransporte", SqlDbType.Int).Value = viagem.TipoTransporte;
                _command.Parameters.AddWithValue("@StatusViagem", SqlDbType.Int).Value = viagem.StatusViagem;
                _command.Parameters.AddWithValue("@MatriculaAprovador", SqlDbType.Int).Value = viagem.MatriculaAprovador;
                _command.Parameters.AddWithValue("@MatriculaSolicitante", SqlDbType.Int).Value = viagem.MatriculaSolicitante;
                _command.Parameters.AddWithValue("@TipoUsuario", SqlDbType.Int).Value = TipoDeUsuario.Administrador;

               var linhasAfetadas =  await _command.ExecuteNonQueryAsync();

                if (linhasAfetadas <= 0)
                    return false;


                return true;
            }
            catch (SqlException)
            {
                // a caso o usuario tente cadastrar a propria matricula dele como aprovador e ele não seja ADM,
                // então vai cair aqui como dbNULL. do campo MatriculaAprovado


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
        public async Task<List<ViagemConsulta>> ConsultaViagem(int matricula)
        {
            ViagemConsultas = new List<ViagemConsulta>();
            try
            {
                _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"SELECT Id, Destino,DataIda, DataVolta,DataSolicitacao,
                                       TipoTransporte,StatusViagem,DataAprovacaoRecusa
                                       FROM dbo.Viagem
                                       WHERE MatriculaSolicitante = @Matricula and StatusViagem != @StatusViagemCancelada";


                _command.Parameters.AddWithValue("@Matricula", SqlDbType.Int).Value = matricula;
                _command.Parameters.AddWithValue("@StatusViagemCancelada", SqlDbType.Int).Value = StatusViagem.Cancelada;

                var dadosDoBanco = await _command.ExecuteReaderAsync();
                ViagemConsulta viagemConsulta = null!;

                while (dadosDoBanco.Read())
                {

                    
                    viagemConsulta = new
                        ViagemConsulta
                        (
                          (int)dadosDoBanco["Id"],
                          (string)dadosDoBanco["Destino"],
                          (DateTime)dadosDoBanco["DataIda"],
                          (DateTime)dadosDoBanco["DataVolta"],
                          (DateTime)dadosDoBanco["DataSolicitacao"],
                          (TipoTransporte)(int)dadosDoBanco["TipoTransporte"],
                          (StatusViagem)(int)dadosDoBanco["StatusViagem"],
                          dadosDoBanco["DataAprovacaoRecusa"]
                        );

                    ViagemConsultas.Add(viagemConsulta);
                }

               
                return ViagemConsultas;
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

        public async Task<bool> CancelarViagem(int IdViagem)
        {
            try
            {
                _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"UPDATE dbo.Viagem
                                         SET StatusViagem = @StatusViagem,
                                         WHERE Id = @Id";

                _command.Parameters.AddWithValue("@Id", SqlDbType.Int).SqlValue = IdViagem;
                _command.Parameters.AddWithValue("@StatusViagem", SqlDbType.Int).SqlValue = StatusViagem.Cancelada;


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

        public async Task<bool> AprovarViagem(int IdViagem)
        {
            try
            {
                _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"UPDATE dbo.Viagem
                                         SET StatusViagem = @StatusViagem
                                         WHERE Id = @Id";

                _command.Parameters.AddWithValue("@Id", SqlDbType.Int).SqlValue = IdViagem;
                _command.Parameters.AddWithValue("@StatusViagem", SqlDbType.Int).SqlValue = StatusViagem.Aprovado;


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
