using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.Models;
using OrderManagerAPI.DALBaseSQL;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace OrderManagerAPI.DALProductSQL
{
    public class DALProduct : DALBase
    {
        public DALProduct(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// Cria uma nova Produto no banco de dados.
        /// </summary>
        /// <returns>
        /// Retorna <c>true</c> se a Produto for criada com sucesso; 
        /// caso contrário, <c>false</c> se ocorrer um erro durante a criação.
        /// </returns>
        /// ajustar ainda n ta pronto
        public bool CreateProductDB(Order order)
        {
            int linhasAfetadas = 0;

            try
            {
                Connection.Open();

                using (var transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmdOrder = new SqlCommand(
                            "INSERT INTO [PRODUCT] (PRODUCTCODE, PRODUCTDESCRIPTION, IMAGE, CYCLETIME) VALUES (@PRODUCTCODE, @PRODUCTDESCRIPTION, @IMAGE, @CYCLETIME)",
                            Connection,
                            transaction))
                        {
                            cmdOrder.Parameters.AddWithValue("@PRODUCTCODE", order.ProductCode);
                            cmdOrder.Parameters.AddWithValue("@PRODUCTDESCRIPTION", order.ProductDescription);
                            cmdOrder.Parameters.AddWithValue("@IMAGE", order.Image);
                            cmdOrder.Parameters.AddWithValue("@CYCLETIME", order.CycleTime);

                            linhasAfetadas = cmdOrder.ExecuteNonQuery();
                        }                       

                        transaction.Commit();

                        return linhasAfetadas > 0;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao criar produto, verifique os dados.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar a Produto no banco de dados.", ex);
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
        /// Retorna o produto
        /// </summary>
        /// <param name="ProductCode">codigo do produto</param>
        /// <exception cref="Exception"></exception>
        public Order GetProdutoDB(string ProductCode)
        {
            Order? order = null;

            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Product] WHERE [ProductCode] = @ProductCode", Connection))
                {
                    cmd.Parameters.AddWithValue("@ProductCode", ProductCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            order = new()
                            {
                                ProductCode = reader.GetString(0),
                                ProductDescription = reader.GetString(1),
                                Image = reader.GetString(2),
                                CycleTime = Convert.ToDouble(reader.GetDecimal(3)),

                            };
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao encontrar Produto, verifique o Codigo do produto", ex);
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
        /// Recupera os Produtos do banco de dados.
        /// </summary>
        /// <returns>
        /// Retorna uma lista de objetos <see cref="Order"/> contendo os Produtos encontradas. 
        /// Se nenhum Produto for encontrado, retorna uma lista vazia.
        /// </returns>
        public List<Order> GetListProdutoDB()
        {
            var listOrders = new List<Order>();

            try
            {
                Connection.Open();

                using (var cmd = new SqlCommand("SELECT * FROM [Product]", Connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order
                            {
                                ProductCode = reader.GetString(0),
                                ProductDescription = reader.GetString(1),
                                Image = reader.GetString(2),
                                CycleTime = (double)reader.GetDecimal(3)
                            };

                            listOrders.Add(order);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar produtos. Verifique se tem produtos cadastrado no banco de dados", ex);
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
        /// Valida o codigo do produto
        /// </summary>
        /// <param name="code">Code Product da O.S</param>
        /// <returns>
        ///  Retorna <c>true</c> se a Produto for Valido; 
        /// caso contrário, <c>false</c> se o produto não existir.
        /// </returns>
        /// <exception cref="Exception"></exception>
        public bool validateCodeProduct(string code)
        {
            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM [Product] WHERE [ProductCode] = @Code", Connection))
                {
                    cmd.Parameters.AddWithValue("@Code", code);

                    //registros correspondentes
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao validar o código do produto. Verifique o código fornecido.", ex);
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
        /// Pega último número do Produto
        /// </summary>
        /// <returns>Acrescentar +1 || retorna valor padrão</returns>
        public string GetLastProduct()
        {
            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT MAX([PRODUCTCODE]) FROM [PRODUCT]", Connection))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        string lastOS = result.ToString();
                        string numericPart = lastOS.Substring(1);
                        int nextNumber = int.Parse(numericPart) + 1;

                        // Mantém o formato string
                        return $"P{nextNumber:D3}";
                    }
                    else
                    {
                        // valor padrão
                        return "P001";
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
        /// Atualiza os dados de um produto no banco de dados.
        /// </summary>
        /// <param name="order">Objeto <see cref="Order"/> contendo os novos dados de um produto a serem atualizados.</param>
        /// <returns>
        /// Retorna <c>true</c> se o produto foi atualizada com sucesso; 
        /// caso contrário, <c>false</c> se a atualização falhar.
        /// </returns>
        public bool EditeProduct(Order order)
        {
            int linhasAfetadas = 0;

            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE [PRODUCT] SET PRODUCTDESCRIPTION = @PRODUCTDESCRIPTION, IMAGE = @IMAGE, CYCLETIME = @CYCLETIME WHERE [PRODUCTCODE] = @PRODUCTCODE", Connection))
                {
                    cmd.Parameters.AddWithValue("@PRODUCTCODE", order.ProductCode);
                    cmd.Parameters.AddWithValue("@PRODUCTDESCRIPTION", order.ProductDescription);
                    cmd.Parameters.AddWithValue("@IMAGE", order.Image);
                    cmd.Parameters.AddWithValue("@CYCLETIME", order.CycleTime);

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }

                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na edição do Produto: {ex.Message}");
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
        /// Remove um Produto do banco de dados com base no número da ordem (OS).
        /// </summary>
        /// <param name="ProductCode">O número identificador único doProduto que será removida.</param>
        /// <returns>
        /// Retorna <c>true</c> se o Produto foi removido com sucesso; 
        /// caso contrário, <c>false</c> se a remoção falhar.
        /// </returns>
        public bool DeleteProduct(string ProductCode)
        {
            int linhasAfetadas = 0;
            try
            {
                Connection.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM [PRODUCT] WHERE [PRODUCTCODE] = @PRODUCTCODE", Connection))
                {
                    cmd.Parameters.AddWithValue("@PRODUCTCODE", ProductCode);

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
                return linhasAfetadas > 0;
            }

            catch (Exception ex)
            {
                throw new Exception("Erro na deleção do Produto.", ex);
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