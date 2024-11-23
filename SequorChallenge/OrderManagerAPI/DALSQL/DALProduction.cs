using OrderManagerAPI;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using OrderManagerAPI.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Data;
using OrderManagerAPI.DALBaseSQL;
using static System.Net.Mime.MediaTypeNames;

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
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return productions;
        }


    }
}
