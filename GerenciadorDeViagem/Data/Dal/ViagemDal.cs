using GerenciadorDeViagem.Model;
using GerenciadorDeViagem.Model.Enum;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GerenciadorDeViagem.Data.Dao
{
    public class ViagemDal
    {
        private readonly Banco _connection;
        private readonly SqlCommand _command;
        public record ViagemConsulta(string destino, DateTime dataIda, DateTime dataVolta, DateTime dataSolicitacao,
                                     TipoTransporte tipoTransporte, StatusViagem statusViagem, Object DataAprovacaoRecusa);
        public ViagemDal()
        {
            _connection = new Banco();
            _command = new SqlCommand();
            _command.CommandType = CommandType.Text;
        }

        public async Task<bool> CadastrarViagem(Viagem viagem)
        {
            try
            {
               _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"INSERT INTO dbo.Viagem
                                       (Destino, DataIda, DataVolta,DataSolicitacao, TipoTransporte, StatusViagem,MatriculaAprovador,MatriculaSolicitante)
                                       VALUES
                                       (@Destino, @DataIda, @DataVolta,@DataSolicitacao, @TipoTransporte, @StatusViagem,@MatriculaAprovador,@MatriculaSolicitante)";


                _command.Parameters.AddWithValue("@Destino", SqlDbType.VarChar).Value = viagem.Destino;
                _command.Parameters.AddWithValue("@DataIda", SqlDbType.DateTime).Value = viagem.DataIda;
                _command.Parameters.AddWithValue("@DataVolta", SqlDbType.DateTime).Value = viagem.DataVolta;
                _command.Parameters.AddWithValue("@DataSolicitacao", SqlDbType.DateTime).Value = viagem.DataDaSolicitacao;
                _command.Parameters.AddWithValue("@TipoTransporte", SqlDbType.Int).Value = viagem.TipoTransporte;
                _command.Parameters.AddWithValue("@StatusViagem", SqlDbType.Int).Value = viagem.StatusViagem;
                _command.Parameters.AddWithValue("@MatriculaAprovador", SqlDbType.Int).Value = viagem.MatriculaAprovador;
                _command.Parameters.AddWithValue("@MatriculaSolicitante", SqlDbType.Int).Value = viagem.MatriculaSolicitante;

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
        public async Task<ViagemConsulta> ConsultaViagem(int matricula)
        {
            try
            {
                _command.Connection = _connection.AbrirConexao();

                _command.CommandText = @"SELECT Destino,DataIda, DataVolta,DataSolicitacao,
                                       TipoTransporte,StatusViagem,DataAprovacaoRecusa
                                       FROM dbo.Viagem
                                       WHERE MatriculaSolicitante = @Matricula";


                _command.Parameters.AddWithValue("@Matricula", SqlDbType.Int).Value = matricula;

                var dadosDoBanco = await _command.ExecuteReaderAsync();
                ViagemConsulta viagemConsulta = null!;

                while (dadosDoBanco.Read())
                {

                    
                    viagemConsulta = new
                        ViagemConsulta
                        (
                          (string)dadosDoBanco["Destino"],
                          (DateTime)dadosDoBanco["DataIda"],
                          (DateTime)dadosDoBanco["DataVolta"],
                          (DateTime)dadosDoBanco["DataSolicitacao"],
                          (TipoTransporte)(int)dadosDoBanco["TipoTransporte"],
                          (StatusViagem)(int)dadosDoBanco["StatusViagem"],
                          dadosDoBanco["DataAprovacaoRecusa"]
                        );
                }

                return viagemConsulta;
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
    }
}
