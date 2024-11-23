using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNetCore.DataProtection;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace OrderManagerAPI.DALBaseSQL
{
    public abstract class DALBase : IDisposable
    {
        protected SqlConnection Connection { get; private set; }
        protected readonly IConfiguration _Configuration;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="DALBase"/> com a conexão ao banco de dados SQL Server.
        /// </summary>
        /// <remarks>
        /// A string de conexão é obtida a partir do arquivo de configuração da aplicação, utilizando a chave "conexao_com_banco_sqlserver".
        /// </remarks>
        public DALBase(IConfiguration configuration)
        {
            _Configuration = configuration;
            string strConnection = _Configuration.GetConnectionString("conexao_com_banco_sqlserver");
            Connection = new SqlConnection(strConnection);
        } 

        /// <summary>
        /// Libera conexão se existir
        /// </summary>
        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }
        }
    }
}
