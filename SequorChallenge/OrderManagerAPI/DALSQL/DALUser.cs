using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.Models;
using OrderManagerAPI.DALBaseSQL;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace OrderManagerAPI.DALUserSQL
{
    public class DALUser : DALBase
    {
        public DALUser(IConfiguration configuration) : base(configuration) { }

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
        /// Valida o codigo do produto
        /// </summary>
        /// <param name="code">Code User da O.S</param>
        /// <returns>Retorna True se o codigo do produto é valido</returns>
        public bool validateEmailUser(string Email)
        {
            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM [User] WHERE [Email] = @Email", Connection))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
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
        /// Busca um usuario específica no banco de dados com base nos dados fornecidos.
        /// </summary>
        /// <returns>
        /// Um objeto do tipo <see cref="User"/> representando os detalhes da usuario encontrada no banco de dados. 
        /// Retorna <c>null</c> se nenhum usario correspondente for encontrada.
        /// </returns>
        /// <exception cref="Exception">
        /// Lançada para outros tipos de erros não relacionados ao banco de dados.
        /// </exception>
        public User FindUser(string email)
        {
            User newUser = null;

            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                "SELECT * FROM [User] WHERE EMAIL = @Email", Connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            newUser = new User
                            {  
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                InitialDate = reader.GetDateTime("InitialDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                               
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

            return newUser;

        }
    }
}
