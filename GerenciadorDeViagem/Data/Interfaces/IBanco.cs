using Microsoft.Data.SqlClient;

namespace GerenciadorDeViagem.Data.Interfaces
{
    public interface IBanco
    {
        SqlConnection AbrirConexao();
        void FecharConexao();
    }
}
