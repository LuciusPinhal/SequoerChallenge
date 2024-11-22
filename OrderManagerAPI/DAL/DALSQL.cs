using OrderManagerAPI;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using OrderManagerAPI.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Data;

namespace OrderManagerAPI.DALSQl
{
    public class DALSQlServer : IDisposable
    {
        private SqlConnection connection;
        private readonly IConfiguration _configuration;

        public DALSQlServer(IConfiguration configuration)
        {
            _configuration = configuration;
            string strConnection = _configuration.GetConnectionString("conexao_com_banco_sqlserver");
            connection = new SqlConnection(strConnection);
        }

        /// <summary>
        /// Valida se o banco de dados SequorDB existe
        /// </summary>
        public void DatabaseExists()
        {
            try
            {
                // Conecta ao banco master
                var builder = new SqlConnectionStringBuilder(connection.ConnectionString)
                {
                    InitialCatalog = "master" // Define o banco master para a verificação
                };

                using (var masterConnection = new SqlConnection(builder.ConnectionString))
                {
                    masterConnection.Open();

                    // Extrai o nome do banco de dados
                    var databaseName = new SqlConnectionStringBuilder(connection.ConnectionString).InitialCatalog;

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
        /// Criando o Banco de dados SequorDB
        /// </summary>
        private void CreateDatabase()
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connection.ConnectionString)
                {
                    InitialCatalog = "master" 
                };

                using (var masterConnection = new SqlConnection(builder.ConnectionString))
                {
                    masterConnection.Open();

                    // Verificar se o banco de dados existe
                    var databaseName = new SqlConnectionStringBuilder(connection.ConnectionString).InitialCatalog;

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
        /// Executa Script de Criação de Tabelas no Banco SequorDB
        /// </summary>
        /// <exception cref="FileNotFoundException">Não Encontrou o Script de criação</exception>
        /// <exception cref="Exception">Erros de execução</exception>
        public void ExecuteScript()
        {
            try
            {
                connection.Open();

                string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

                // Caminho do arquivo de script bin
                string scriptPath = Path.Combine(projectRoot, "Scripts", "CreateTables.sql");

                // Verifica se o arquivo existe
                if (!File.Exists(scriptPath))
                {
                    throw new FileNotFoundException($"O arquivo de script SQL não foi encontrado: {scriptPath}");
                }

                // Lê o conteúdo do arquivo SQL
                string script = File.ReadAllText(scriptPath);

                // Executa o script no banco
                using (var cmd = new SqlCommand(script, connection))
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
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }


        public List<Order> GetOrdersDB()
        {

            List<Order> listOrders = new List<Order>();
            Material material = null;
            Order order = null;

            //try
            //{
            //    connection.Open();

            //    using (SqlCommand cmd = new SqlCommand(
            //               "SELECT loja.id, loja.nome, loja.cidade, section.nome AS nome_secao, produto.nome AS nome_produto, produto.descricao, produto.valor, section.id AS section_id " +
            //               "FROM public.loja " +
            //               "LEFT JOIN public.section ON loja.id = section.loja_id " +
            //               "LEFT JOIN public.produto ON section.id = produto.section_id", connection))
            //    {



            //        using (SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                //int lojaId = reader.GetInt32(0);

            //                //Models.Loja loja = listLoja.FirstOrDefault(l => l.Id == lojaId);

            //                if (order == null || order.Id != reader.GetInt32(0))
            //                {
            //                    order = new Order
            //                    {
            //                        Id = reader.GetInt32(0),
            //                        Nome = reader.GetString(1),
            //                        Cidade = reader.GetString(2),
            //                        Sections = new List<Api_Store.Section>()
            //                    };

            //                    listLoja.Add(loja);

            //                }

            //                if (!reader.IsDBNull(3))
            //                {
            //                    int sectionId = reader.GetInt32(7);
            //                    section = loja.Sections.FirstOrDefault(s => s.Id == sectionId);

            //                    // Seção não encontrada pelo ID, verificar pelo nome
            //                    if (section == null || section.Nome != reader.GetString(3))
            //                    {
            //                        section = loja.Sections.FirstOrDefault(s => s.Nome == reader.GetString(3));

            //                        if (section == null)
            //                        {
            //                            section = new Api_Store.Section
            //                            {
            //                                Id = sectionId,
            //                                Nome = reader.GetString(3),
            //                                Produtos = new List<Produto>()
            //                            };

            //                            loja.Sections.Add(section);
            //                        }
            //                    }
            //                }

            //                if (!reader.IsDBNull(4))
            //                {
            //                    Produto produto = new Produto()
            //                    {
            //                        Nome = reader.GetString(4),
            //                        Descricao = reader.GetString(5),
            //                        Valor = reader.GetDouble(6),

            //                    };

            //                    section.Produtos.Add(produto);
            //                }

            //            }

            //            return listLoja;
            //        }
            //    }
            //}

            //catch (Exception ex)
            //{

            //    throw;
            //}
            //finally
            //{
            //    if (connection.State == System.Data.ConnectionState.Open)
            //    {
            //        connection.Close();
            //    }
            //}
            return null;
        }

        public List<Production> GetProductionDB()
        {
            List<Production> productions = new List<Production>();

            try
            {
                connection.Open();

                string query = @"
                        SELECT 
                            Production.Email, 
                            Production.[Order], 
                            Production.Date, 
                            Production.Quantity, 
                            Production.MaterialCode, 
                            Production.CycleTime 
                        FROM Production";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var production = new Production
                        {
                            Email = reader.GetString(0),
                            Order = reader.GetString(1),
                            ProductionDate = reader.GetDateTime(2).ToString("yyyy-MM-dd"), 
                            ProductionTime = reader.GetDateTime(2).ToString("HH:mm:ss"), 
                            Quantity = (double)reader.GetDecimal(3),
                            materialCode = reader.GetString(4),
                            CycleTime = (double)reader.GetDecimal(5)
                        };

                        productions.Add(production); 
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Erro ao buscar dados de produção.", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return productions;
        }

        public void Dispose()
        {
            // Liberando a conexão com o banco de dados, se existir
            if (connection != null)
            {
                connection.Dispose();
                connection = null; 
            }
        }

    }
}
