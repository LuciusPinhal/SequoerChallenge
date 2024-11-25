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
        /// ajustar - Cria uma nova Produto no banco de dados.
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
                        // Order
                        using (SqlCommand cmdOrder = new SqlCommand(
                            "INSERT INTO [Order] ([Order], Quantity, ProductCode) VALUES (@Order, @Quantity, @ProductCode)",
                            Connection,
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
                            Connection,
                            transaction))
                        {
                            //ajustar o produto code, n pode estar repedito
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
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
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

    }
}