using OrderManagerAPI;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using OrderManagerAPI.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

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
        /// Executa Script de Criação de Tabelas e PROCS no Banco SequorDB
        /// </summary>
        /// <exception cref="FileNotFoundException">Não Encontrou o Script de criação</exception>
        /// <exception cref="Exception">Erros de execução</exception>
        public void ExecuteScript()
        {
            try
            {
                connection.Open();

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
                using (var cmd = new SqlCommand(createTablesScript, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                // Executa PROCS
                string prcGetOrderDetailsByEmailScript = File.ReadAllText(prcGetOrderDetailsByEmailScriptPath);

                using (var cmd = new SqlCommand(prcGetOrderDetailsByEmailScript, connection))
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

        /// <summary>
        /// Retorna a lista de O.S pela PROC PRCGetOrderDetailsByEmail
        /// </summary>
        /// <param name="email">Email do Usuario</param>
        /// <returns>Lista de O.S com os apontamentos da SetProduction</returns>
        /// <exception cref="Exception"></exception>
        public List<Order> GetOrdersDB(string email)
        {
            List<Order> listOrders = new List<Order>();

            try
            {
                connection.Open();

                using (var cmd = new SqlCommand("PRCGetOrderDetailsByEmail", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Verifique se a ordem já existe na lista
                            var existingOrder = listOrders.FirstOrDefault(o => o.OS == reader["O.S"].ToString());

                            if (existingOrder == null)
                            {
                                // Se a ordem não existir, cria uma nova ordem
                                existingOrder = new Order
                                {
                                    OS = reader["O.S"].ToString(),
                                    Quantity = Convert.ToDouble(reader["quantity"]),
                                    ProductCode = reader["productCode"].ToString(),
                                    ProductDescription = reader["productDescription"].ToString(),
                                    Image = reader["Image"] != DBNull.Value ? reader["Image"].ToString() : null,
                                    CycleTime = reader["cycleTime"] != DBNull.Value ? Convert.ToDouble(reader["cycleTime"]) : 0,
                                    Materials = new List<Material>()
                                };

                                // Adiciona a ordem à lista
                                listOrders.Add(existingOrder);
                            }

                            // Adiciona o material à lista de materiais da ordem
                            existingOrder.Materials.Add(new Material
                            {
                                MaterialCode = reader["materialCode"].ToString(),
                                MaterialDescription = reader["materialDescription"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar dados de produção.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return listOrders;
        }

        /// <summary>
        /// Retorna a tabela de Produção
        /// </summary>
        /// <param name="email">Email User</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Production> GetProductionDB(string email)
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
                        FROM Production
                        WHERE[Email] = @Email";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
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

        /// <summary>
        /// Pega último número da O.S
        /// </summary>
        /// <returns>Acrescentar +1 || retorna valor padrão</returns>
        /// <exception cref="Exception"></exception>
        public string GetLastOS()
        {
            try
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT MAX([Order]) FROM [Order]", connection))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {                    
                        string lastOS = result.ToString();
                        string numericPart = lastOS.Substring(1); 
                        int nextNumber = int.Parse(numericPart) + 1;

                       // Mantém o formato string
                        return $"O{nextNumber:D3}"; 
                    }
                    else
                    {
                        // valor padrão
                        return "O001";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a última ordem.", ex);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Criando O.S
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool CreateOrder(Order order)
        {
            int linhasAfetadas = 0;

            try
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Order
                        using (SqlCommand cmdOrder = new SqlCommand(
                            "INSERT INTO [Order] ([Order], Quantity, ProductCode) VALUES (@Order, @Quantity, @ProductCode)",
                            connection,
                            transaction))
                        {
                            cmdOrder.Parameters.AddWithValue("@Order", order.OS);
                            cmdOrder.Parameters.AddWithValue("@Quantity", order.Quantity);
                            cmdOrder.Parameters.AddWithValue("@ProductCode", order.ProductCode);

                            linhasAfetadas += cmdOrder.ExecuteNonQuery();
                        }

                        // Product
                        using (SqlCommand cmdProduct = new SqlCommand(
                            "INSERT INTO Product (ProductCode, ProductDescription, Image, CycleTime) " +
                            "VALUES (@ProductCode, @ProductDescription, @Image, @CycleTime);",
                            connection,
                            transaction))
                        {
                            cmdProduct.Parameters.AddWithValue("@productCode", order.ProductCode);
                            cmdProduct.Parameters.AddWithValue("@productDescription", order.ProductDescription);
                            cmdProduct.Parameters.AddWithValue("@Image", order.Image);
                            cmdProduct.Parameters.AddWithValue("@cycleTime", order.CycleTime);

                            linhasAfetadas += cmdProduct.ExecuteNonQuery();
                        }
                        

                        transaction.Commit();

                        return linhasAfetadas == 2; 
                    }
                    catch
                    {           
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar a ordem no banco de dados.", ex);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }



        /// <summary>
        /// Libera conexão se existir
        /// </summary>
        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null; 
            }
        }

    }
}
