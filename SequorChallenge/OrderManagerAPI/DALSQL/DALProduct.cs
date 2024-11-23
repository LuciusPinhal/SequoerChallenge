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
        /// fazer controller do produto, fazer o crud la e tbm o produto n poder ter o code igual
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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


    }
}
