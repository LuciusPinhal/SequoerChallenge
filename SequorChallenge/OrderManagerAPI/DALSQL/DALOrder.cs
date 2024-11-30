using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.Models;
using OrderManagerAPI.DALBaseSQL;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using static System.Collections.Specialized.BitVector32;

namespace OrderManagerAPI.DALOrderSQL
{
    public class DALOrder : DALBase
    {
        public DALOrder(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Recupera uma ou mais ordens pela PROC PRCGetOrderDetailsByEmail
        /// </summary>
        /// <param name="email">Email do Usuario</param>
        /// <returns>Lista de O.S com os apontamentos da SetProduction</returns>
        /// <exception cref="Exception"></exception>
        public List<Order> GetOrdersDB(string? email = null)
        {
            List<Order> listOrders = new List<Order>();

            try
            {

                Connection.Open();

                using (var cmd = new SqlCommand())
                {

                    cmd.Connection = Connection;

                    if (!string.IsNullOrEmpty(email))
                    {
                        cmd.CommandText = "PRCGetOrderDetailsByEmail"; 
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", email);
                    }
                    else
                    {
                        cmd.CommandText = "PRCGetOrderDetails";
                        cmd.CommandType = CommandType.StoredProcedure;
                    }

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
                throw new Exception("Erro ao buscar dados da ordem.", ex);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return listOrders;
        }

        public List<Order> GetOSDB()
        {
            List<Order> listOrders = new List<Order>();

            try
            {
                Connection.Open();

                using (var cmd = new SqlCommand("SELECT * FROM [ORDER]", Connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order
                            {

                                OS = reader["Order"].ToString(),
                                Quantity = Convert.ToDouble(reader["quantity"]),
                                ProductCode = reader["productCode"].ToString(),
                                ProductDescription = "",
                                Image = "",
                                CycleTime =  0,
                                Materials = new List<Material>()


                            };

                            listOrders.Add(order);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar Ordens. Verifique se tem Ordens cadastrada no banco de dados", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return listOrders;
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
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT MAX([Order]) FROM [Order]", Connection))
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
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        ///  Insere uma nova ordem no banco de dados.
        /// </summary>
        /// <param name="order">Objeto <see cref="Order"/> contendo os dados da nova ordem a ser criada.</param
        /// <exception cref="Exception">Lançada quando ocorre um erro ao executar o comando SQL, como falhas de conexão ou violação de restrições.</exception>
        public bool CreateOrderDB(Order order)
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
                            "INSERT INTO [Order] ([Order], Quantity, ProductCode) VALUES (@Order, @Quantity, @ProductCode)",
                            Connection,
                            transaction))
                        {
                            cmdOrder.Parameters.AddWithValue("@Order", order.OS);
                            cmdOrder.Parameters.AddWithValue("@Quantity", order.Quantity);
                            cmdOrder.Parameters.AddWithValue("@ProductCode", order.ProductCode);

                            linhasAfetadas = cmdOrder.ExecuteNonQuery();
                        }


                        transaction.Commit();

                        return linhasAfetadas > 0;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao criar ordem.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar a ordem no banco de dados.", ex);
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
        /// Verifica se a OS é valida
        /// </summary>
        /// <param name="OS">Número da OS</param>
        /// <returns>Retorna a OS</returns>
        public bool VerifyOrderDB(string OS)
        {     
            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM [Order] WHERE [order] = @os", Connection))
                {
                    cmd.Parameters.AddWithValue("@os", OS);
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao encontrar Order, verifique o número", ex);
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
        /// Pegar a OS pelo número
        /// </summary>
        /// <param name="OS">Número da OS</param>
        /// <returns>Retorna a OS</returns>
        public Order GetOrderDB(string OS)
        {
            Order? order = null;

            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Order] WHERE [order] = @os", Connection))
                {
                    cmd.Parameters.AddWithValue("@os", OS);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            order = new()
                            {
                                OS = reader.GetString(0),
                                Quantity = (double)reader.GetDecimal(1),
                                ProductCode = reader.GetString(2)

                            };
                            reader.Close();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao encontrar Order, verifique o número", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            return order;
        }

        /// <summary>
        /// Atualiza os dados de uma ordem no banco de dados.
        /// </summary>
        /// <param name="order">Objeto <see cref="Order"/> contendo os novos dados da ordem a serem atualizados.</param>
        /// <returns>
        /// Retorna <c>true</c> se a ordem foi atualizada com sucesso; 
        /// caso contrário, <c>false</c> se a atualização falhar.
        /// </returns>
        public bool EditeOrder(Order order)
        {
            int linhasAfetadas = 0;

            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE [Order] SET Quantity = @quantity, ProductCode = @productCode WHERE [Order] = @OS", Connection))
                {             
                    cmd.Parameters.AddWithValue("OS", order.OS);
                    cmd.Parameters.AddWithValue("quantity", order.Quantity);
                    cmd.Parameters.AddWithValue("productCode", order.ProductCode);

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
        /// Remove uma ordem do banco de dados com base no número da ordem (OS).
        /// </summary>
        /// <param name="os">O número identificador único da ordem (OS) que será removida.</param>
        /// <returns>
        /// Retorna <c>true</c> se a ordem foi removida com sucesso; 
        /// caso contrário, <c>false</c> se a remoção falhar.
        /// </returns>
        public bool DeleteOrder(string Order)
        {
            int linhasAfetadas = 0;
            try
            {
                Connection.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM [Order] WHERE [Order] = @OS", Connection))
                {
                    cmd.Parameters.AddWithValue("@OS", Order);

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
                return linhasAfetadas > 0;
            }

            catch (Exception ex)
            {
                throw new Exception("Erro na deleção da Ordem.", ex);
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
