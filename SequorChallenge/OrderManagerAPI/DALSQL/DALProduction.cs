using OrderManagerAPI;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using OrderManagerAPI.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Data;
using OrderManagerAPI.DALBaseSQL;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Collections.Specialized.BitVector32;
using System.Globalization;

namespace OrderManagerAPI.DALProductionSQL
{
    public class DALProduction : DALBase
    {
        public DALProduction(IConfiguration configuration) : base(configuration) { }

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
                Connection.Open();

                string query = @"
                        SELECT 
                            Production.ID,
                            Production.Email, 
                            Production.[Order], 
                            Production.Date, 
                            Production.Quantity, 
                            Production.MaterialCode, 
                            Production.CycleTime 
                        FROM Production
                        WHERE[Email] = @Email";

                using (SqlCommand cmd = new SqlCommand(query, Connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            var production = new Production
                            {
                                Id = reader.GetInt64(0),
                                Email = reader.GetString(1),
                                Order = reader.GetString(2),
                                ProductionDate = reader.GetDateTime(3).ToString("yyyy-MM-dd"), 
                                ProductionTime = reader.GetDateTime(3).ToString("HH:mm:ss"), 
                                Quantity = (double)reader.GetDecimal(4),
                                materialCode = reader.GetString(5),
                                CycleTime = (double)reader.GetDecimal(6)
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
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return productions;
        }

        /// <summary>
        ///  Insere uma nova Produção no banco de dados.
        /// </summary>
        /// <param name="order">Objeto <see cref="Order"/> contendo os dados da nova Produção a ser criada.</param
        /// <exception cref="Exception">Lançada quando ocorre um erro ao executar o comando SQL, como falhas de conexão ou violação de restrições.</exception>
        public bool CreateProductionDB(Production production)
        {
            int linhasAfetadas = 0;

            try
            {
                Connection.Open();

                using (var transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        // Order
                        using (SqlCommand cmdOrder = new SqlCommand(
                            "INSERT INTO [production] ([Email], [Order], Date, Quantity, MaterialCode, CycleTime) " +
                            "VALUES (@Email, @Order, @Date, @Quantity, @MaterialCode, @CycleTime )",
                            Connection, transaction))
                        {

                            string productionDateTime = $"{production.ProductionDate} {production.ProductionTime}";
                            DateTime dateTimeValue = DateTime.ParseExact(productionDateTime, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                            cmdOrder.Parameters.AddWithValue("@Email", production.Email);
                            cmdOrder.Parameters.AddWithValue("@Order", production.Order);
                            cmdOrder.Parameters.AddWithValue("@Date", dateTimeValue);
                            cmdOrder.Parameters.AddWithValue("@Quantity", production.Quantity);
                            cmdOrder.Parameters.AddWithValue("@MaterialCode", production.materialCode);
                            cmdOrder.Parameters.AddWithValue("@CycleTime", production.CycleTime);
                           

                            linhasAfetadas = cmdOrder.ExecuteNonQuery();
                        }


                        transaction.Commit();

                        return linhasAfetadas > 0;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new Exception("Ocorreu erro ao criar a Produção, Verifique os Dados");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar a produção no banco de dados.", ex);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Busca uma produção específica no banco de dados com base nos dados fornecidos.
        /// </summary>
        /// <param name="production">Objeto do tipo <see cref="Production"/> contendo os critérios de busca, como Email, Ordem, Data, entre outros.</param>
        /// <returns>
        /// Um objeto do tipo <see cref="Production"/> representando os detalhes da produção encontrada no banco de dados. 
        /// Retorna <c>null</c> se nenhuma produção correspondente for encontrada.
        /// </returns>
        /// <exception cref="Exception">
        /// Lançada para outros tipos de erros não relacionados ao banco de dados.
        /// </exception>
        public Production FindProduction(long ID)
        {
            Production newProduction = null;

            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                "SELECT * FROM Production WHERE ID = @ID", Connection))
                {                
                    cmd.Parameters.AddWithValue("@ID", ID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            newProduction = new Production
                            {
                                Id = reader.GetInt64(reader.GetOrdinal("Id")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Order = reader.GetString(reader.GetOrdinal("Order")),
                                ProductionDate = reader.GetDateTime(reader.GetOrdinal("Date")).ToString("yyyy-MM-dd"),
                                ProductionTime = reader.GetDateTime(reader.GetOrdinal("Date")).ToString("HH:mm:ss"),
                                Quantity = Convert.ToDouble(reader["Quantity"]),
                                materialCode = reader.GetString(reader.GetOrdinal("MaterialCode")),
                                CycleTime = Convert.ToDouble(reader["CycleTime"]),

                            };
                            reader.Close();

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
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return newProduction;

        }

        /// <summary>
        /// Atualiza os dados de uma produção no banco de dados.
        /// </summary>
        /// <returns>
        /// Retorna <c>true</c> se a Produção foi atualizada com sucesso; 
        /// caso contrário, <c>false</c> se a atualização falhar.
        /// </returns>
        public bool EditeProduction(Production production)
        {
            int linhasAfetadas = 0;

            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Production SET Email = @Email, [Order] = @OS, Date = @Date, Quantity = @Quantity, MaterialCode = @MaterialCode, CycleTime = @CycleTime" +
                    " WHERE ID = @ID", Connection))
                {

                    string productionDateTime = $"{production.ProductionDate} {production.ProductionTime}";
                    DateTime dateTimeValue = DateTime.ParseExact(productionDateTime, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

                    cmd.Parameters.AddWithValue("ID", production.Id);         
                    cmd.Parameters.AddWithValue("@Email", production.Email);
                    cmd.Parameters.AddWithValue("@OS", production.Order);
                    cmd.Parameters.AddWithValue("@Date", dateTimeValue);
                    cmd.Parameters.AddWithValue("@Quantity", production.Quantity);
                    cmd.Parameters.AddWithValue("@MaterialCode", production.materialCode);
                    cmd.Parameters.AddWithValue("@CycleTime", production.CycleTime);

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }

                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na edição da Ordem: {ex.Message}");
                return false;
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Remove a Production do banco de dados com base ID.
        /// </summary>
        /// <returns>
        /// Retorna <c>true</c> se a Production foi removida com sucesso; 
        /// caso contrário, <c>false</c> se a remoção falhar.
        /// </returns>
        public bool DeleteProduction(long ID)
        {
            int linhasAfetadas = 0;
            try
            {
                Connection.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM [PRODUCTION] WHERE [ID] = @ID", Connection))
                {
                    cmd.Parameters.AddWithValue("@ID", ID);

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
                return linhasAfetadas > 0;
            }

            catch (Exception ex)
            {
                throw new Exception("Erro na deleção da Produção:", ex);
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
