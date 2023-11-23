using Microsoft.Data.SqlClient;

namespace GerenciadorDeViagem.Data.Interfaces
{
    public interface IBanco
    {
        Task<SqlConnection> AbrirConexao();
        Task FecharConexao();
    }
}
