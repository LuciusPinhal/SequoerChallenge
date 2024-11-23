using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using OrderManagerAPI.DALBaseSQL;
using System.Data.SqlClient;

namespace OrderManagerAPI.DALDBSQL
{
    public class DALDataBase : DALBase
    {
        public DALDataBase(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// verificar/criar se o banco de dados SequorBD existe
        /// </summary>
        public void DatabaseExists()
        {
            try
            {
                // Conecta ao banco master
                var builder = new SqlConnectionStringBuilder(Connection.ConnectionString)
                {
                    InitialCatalog = "master" // Define o banco master para a verificação
                };

                using (var masterConnection = new SqlConnection(builder.ConnectionString))
                {
                    masterConnection.Open();

                    // Extrai o nome do banco de dados
                    var databaseName = new SqlConnectionStringBuilder(Connection.ConnectionString).InitialCatalog;

                    // Verifica se o banco de dados existe
                    string checkDbQuery = $@"
                        SELECT CAST(
                            CASE WHEN EXISTS 
                                (SELECT name 
                                 FROM sys.databases 
                                 WHERE name = '{databaseName}') 
                            THEN 1 ELSE 0 END AS BIT)";

                    using (var cmd = new SqlCommand(checkDbQuery, masterConnection))
                    {
                        var exists = (bool)cmd.ExecuteScalar();
                        if (!exists)
                        {
                            Console.WriteLine("Criando Banco de Dados.");
                            CreateDatabase();
                        }
                        Console.WriteLine("Banco de Dados Valido.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar se o banco de dados existe.", ex);
            }
        }

        /// <summary>
        /// Criando o Banco de dados SequorBD
        /// </summary>
        private void CreateDatabase()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(Connection.ConnectionString)
                {
                    InitialCatalog = "master"
                };

                using (var masterConnection = new SqlConnection(builder.ConnectionString))
                {
                    masterConnection.Open();

                    // Verificar se o banco de dados existe
                    var databaseName = new SqlConnectionStringBuilder(Connection.ConnectionString).InitialCatalog;

                    string checkDbQuery = $@"CREATE DATABASE [{databaseName}]";

                    using (var cmd = new SqlCommand(checkDbQuery, masterConnection))
                    {
                        cmd.ExecuteNonQuery();
                        ExecuteScript();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar/criar o banco de dados.", ex);
            }
        }

        /// <summary>
        /// Executa Script de Criação de Tabelas e PROCS no Banco SequorBD
        /// </summary>
        /// <exception cref="FileNotFoundException">Não Encontrou o Script de criação</exception>
        /// <exception cref="Exception">Erros de execução</exception>
        public void ExecuteScript()
        {
            try
            {
                Connection.Open();

                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                string createTablesScriptPath = Path.Combine(projectRoot, "Scripts", "CreateTables.sql");
                string prcGetOrderDetailsByEmailScriptPath = Path.Combine(projectRoot, "Scripts", "PRCGetOrderDetailsByEmail.sql");

                if (!File.Exists(createTablesScriptPath))
                {
                    throw new FileNotFoundException($"O arquivo de script SQL não foi encontrado: {createTablesScriptPath}");
                }

                if (!File.Exists(prcGetOrderDetailsByEmailScriptPath))
                {
                    throw new FileNotFoundException($"O arquivo de script SQL não foi encontrado: {prcGetOrderDetailsByEmailScriptPath}");
                }

                // Executa CreateTables
                string createTablesScript = File.ReadAllText(createTablesScriptPath);
                using (var cmd = new SqlCommand(createTablesScript, Connection))
                {
                    cmd.ExecuteNonQuery();
                }

                // Executa PROC
                string prcGetOrderDetailsByEmailScript = File.ReadAllText(prcGetOrderDetailsByEmailScriptPath);

                using (var cmd = new SqlCommand(prcGetOrderDetailsByEmailScript, Connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao executar o script SQL.", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

    }
}
