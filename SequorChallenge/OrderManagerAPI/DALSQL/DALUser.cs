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
        /// Valida o codigo do usuario
        /// </summary>
        /// <param name="code">Code User da O.S</param>
        /// <returns>Retorna True se o codigo do usuario é valido</returns>
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
                throw new Exception("Erro ao validar o código do usuario. Verifique o código fornecido.", ex);
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

        /// <summary>
        /// Recupera os Usuario do banco de dados.
        /// </summary>
        /// <returns>
        /// Retorna uma lista de objetos <see cref="User"/> contendo os Usuario encontradas. 
        /// Se nenhuma user for encontrada, retorna uma lista vazia.
        /// </returns>
        public List<User> GetListUserDB()
        {
            var listUsers = new List<User>();

            try
            {
                Connection.Open();

                using (var cmd = new SqlCommand("SELECT * FROM [USER]", Connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Email = reader.GetString(0),
                                Name = reader.GetString(1),
                                InitialDate = reader.GetDateTime(2),
                                EndDate = reader.GetDateTime(3)
                            };

                            listUsers.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar Usuarios. Verifique se tem Usuarios cadastrado no banco de dados", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return listUsers;
        }

        /// <summary>
        ///  Insere uma novo usuario no banco de dados.
        /// </summary>
        /// <param name="USer">Objeto <see cref="User"/> contendo os dados da novo usario a ser criada.</param
        /// <exception cref="Exception">Lançada quando ocorre um erro ao executar o comando SQL, como falhas de conexão ou violação de restrições.</exception>
        public bool CreateUserDB(User user)
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
                            "INSERT INTO [USER] ([EMAIL], NAME, InitialDate, EndDate) VALUES (@EMAIL, @NAME, @InitialDate, @EndDate)",
                            Connection,
                            transaction))
                        {
                            cmdOrder.Parameters.AddWithValue("@EMAIL", user.Email);
                            cmdOrder.Parameters.AddWithValue("@NAME", user.Name);
                            cmdOrder.Parameters.AddWithValue("@InitialDate", user.InitialDate);
                            cmdOrder.Parameters.AddWithValue("@EndDate", user.EndDate);

                            linhasAfetadas = cmdOrder.ExecuteNonQuery();
                        }


                        transaction.Commit();

                        return linhasAfetadas > 0;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao criar usuario.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o usuario no banco de dados.", ex);
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
        /// Atualiza os dados de um usario no banco de dados.
        /// </summary>
        /// <param name="User">Objeto <see cref="User"/> contendo os novos dados do usuario a serem atualizados.</param>
        /// <returns>
        /// Retorna <c>true</c> se o usuario foi atualizada com sucesso; 
        /// caso contrário, <c>false</c> se a atualização falhar.
        /// </returns>
        public bool EditeUserDB(User user)
        {
            int linhasAfetadas = 0;

            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE [USER] SET EMAIL = @EMAIL, NAME = @NAME, InitialDate = @InitialDate, EndDate = @EndDate " +
                    "WHERE [EMAIL] = @EMAIL", Connection))
                {
         
                    cmd.Parameters.AddWithValue("@EMAIL", user.Email);
                    cmd.Parameters.AddWithValue("@NAME", user.Name);
                    cmd.Parameters.AddWithValue("@InitialDate", user.InitialDate);
                    cmd.Parameters.AddWithValue("@EndDate", user.EndDate);

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }

                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na edição do Usuario: {ex.Message}");
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
        /// Remove um usuario do banco de dados com base no número da ordem (OS).
        /// </summary>
        /// <param name="Email">O número identificador único do usuario que será removida.</param>
        /// <returns>
        /// Retorna <c>true</c> se o usuario foi removido com sucesso; 
        /// caso contrário, <c>false</c> se a remoção falhar.
        /// </returns>
        public bool DeleteUser(string Email)
        {
            int linhasAfetadas = 0;
            try
            {
                Connection.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM [USER] WHERE [EMAIL] = @EMAIL", Connection))
                {
                    cmd.Parameters.AddWithValue("@EMAIL", Email);

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
                return linhasAfetadas > 0;
            }

            catch (Exception ex)
            {
                throw new Exception("Erro na deleção do usuario.", ex);
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
