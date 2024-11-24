using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.Models;
using OrderManagerAPI.DALBaseSQL;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace OrderManagerAPI.DALMaterialSQL
{
    public class DALMaterial : DALBase
    {
        public DALMaterial(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// ajustar - Cria uma nova User no banco de dados.
        /// </summary>
        /// <param name="order">Objeto <see cref="Order"/> contendo os dados da User a ser criada.</param>
        /// <returns>
        /// Retorna <c>true</c> se a User for criada com sucesso; 
        /// caso contrário, <c>false</c> se ocorrer um erro durante a criação.
        /// </returns>
        //public bool CreateUserDB(Order order)
        //{
        //    int linhasAfetadas = 0;

        //    try
        //    {
        //        Connection.Open();

        //        using (var transaction = Connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Order
        //                using (SqlCommand cmdOrder = new SqlCommand(
        //                    "INSERT INTO [Order] ([Order], Quantity, UserCode) VALUES (@Order, @Quantity, @UserCode)",
        //                    Connection,
        //                    transaction))
        //                {
        //                    cmdOrder.Parameters.AddWithValue("@Order", order.OS);
        //                    cmdOrder.Parameters.AddWithValue("@Quantity", order.Quantity);
        //                    cmdOrder.Parameters.AddWithValue("@UserCode", order);

        //                    linhasAfetadas += cmdOrder.ExecuteNonQuery();
        //                }

        //                // User
        //                using (SqlCommand cmdUser = new SqlCommand(
        //                    "INSERT INTO User (UserCode, UserDescription, Image, CycleTime) " +
        //                    "VALUES (@UserCode, @UserDescription, @Image, @CycleTime);",
        //                    Connection,
        //                    transaction))
        //                {
        //                    //ajustar o produto code, n pode estar repedito
        //                    cmdUser.Parameters.AddWithValue("@UserCode", order.UserCode);
        //                    cmdUser.Parameters.AddWithValue("@UserDescription", order.UserDescription);
        //                    cmdUser.Parameters.AddWithValue("@Image", order.Image);
        //                    cmdUser.Parameters.AddWithValue("@cycleTime", order.CycleTime);

        //                    linhasAfetadas += cmdUser.ExecuteNonQuery();
        //                }

        //                transaction.Commit();

        //                return linhasAfetadas == 2;
        //            }
        //            catch
        //            {
        //                transaction.Rollback();
        //                throw;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Erro ao criar a ordem no banco de dados.", ex);
        //    }
        //    finally
        //    {
        //        if (Connection.State == ConnectionState.Open)
        //        {
        //            Connection.Close();
        //        }
        //    }
        //}

        /// <summary>
        /// Valida o codigo do Material
        /// </summary>
        /// <param name="MaterialCode">Codigo do Material</param>
        /// <returns>Retorna True se o codigo do Material é valido</returns>
        public bool validateMaterialCode(string MaterialCode)
        {
            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM [Material] WHERE [MaterialCode] = @MaterialCode", Connection))
                {
                    cmd.Parameters.AddWithValue("@MaterialCode", MaterialCode);
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

    }
}
