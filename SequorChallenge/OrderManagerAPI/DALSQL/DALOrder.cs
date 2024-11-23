using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.Models;
using OrderManagerAPI.DALBaseSQL;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace OrderManagerAPI.DALOrderSQL
{
    public class DALOrder : DALBase
    {
        public DALOrder(IConfiguration configuration) : base(configuration) { }

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
                Connection.Open();

                using (var cmd = new SqlCommand("PRCGetOrderDetailsByEmail", Connection))
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
                if (Connection.State == ConnectionState.Open)
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
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        /// <summary>
        /// Retorna a O.S 
        /// </summary>

        public Order GetOrderDB(string OS)
        {
        //    List<Order> listOrders = new List<Order>();

        //    try
        //    {
        //        Connection.Open();

        //        using (var cmd = new SqlCommand("PRCGetOrderDetailsByEmail", Connection))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Email", email);

        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    // Verifique se a ordem já existe na lista
        //                    var existingOrder = listOrders.FirstOrDefault(o => o.OS == reader["O.S"].ToString());

        //                    if (existingOrder == null)
        //                    {
        //                        // Se a ordem não existir, cria uma nova ordem
        //                        existingOrder = new Order
        //                        {
        //                            OS = reader["O.S"].ToString(),
        //                            Quantity = Convert.ToDouble(reader["quantity"]),
        //                            ProductCode = reader["productCode"].ToString(),
        //                            ProductDescription = reader["productDescription"].ToString(),
        //                            Image = reader["Image"] != DBNull.Value ? reader["Image"].ToString() : null,
        //                            CycleTime = reader["cycleTime"] != DBNull.Value ? Convert.ToDouble(reader["cycleTime"]) : 0,
        //                            Materials = new List<Material>()
        //                        };

        //                        // Adiciona a ordem à lista
        //                        listOrders.Add(existingOrder);
        //                    }

        //                    // Adiciona o material à lista de materiais da ordem
        //                    existingOrder.Materials.Add(new Material
        //                    {
        //                        MaterialCode = reader["materialCode"].ToString(),
        //                        MaterialDescription = reader["materialDescription"].ToString()
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Erro ao buscar dados de produção.", ex);
        //    }
        //    finally
        //    {
        //        if (Connection.State == ConnectionState.Open)
        //        {
        //            Connection.Close();
        //        }
        //    }

             return null;
        }


        public bool EditeOrder(Order order)
        {
            return false;
        }
}
