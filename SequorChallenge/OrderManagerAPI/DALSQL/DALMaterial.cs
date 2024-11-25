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
                throw new Exception("Erro ao validar o código do Material. Verifique o código fornecido.", ex);
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
        /// Recupera os Materiais do banco de dados.
        /// </summary>
        /// <returns>
        /// Retorna uma lista de objetos <see cref="Material"/> contendo os Materiais encontradas. 
        /// Se nenhum Material for encontrado, retorna uma lista vazia.
        /// </returns>
        public List<Material> GetListMaterialDB()
        {
            var listMaterials = new List<Material>();

            try
            {
                Connection.Open();

                using (var cmd = new SqlCommand("SELECT * FROM [Material]", Connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Material = new Material
                            {
                                MaterialCode = reader.GetString(0),
                                MaterialDescription = reader.GetString(1),                       
                            };

                            listMaterials.Add(Material);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar Materiais. Verifique se tem Materiais cadastrado no banco de dados", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return listMaterials;
        }

        /// <summary>
        /// Pega último número do Material
        /// </summary>
        /// <returns>Acrescentar +1 || retorna valor padrão</returns>
        public string GetLastMaterial()
        {
            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT MAX([MATERIALCODE]) FROM [Material]", Connection))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        string lastOS = result.ToString();
                        string numericPart = lastOS.Substring(1);
                        int nextNumber = int.Parse(numericPart) + 1;

                        // Mantém o formato string
                        return $"M{nextNumber:D3}";
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
                throw new Exception("Erro ao obter a último Material.", ex);
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
        /// Cria uma novo Material no banco de dados.
        /// </summary>
        /// <returns>
        /// Retorna <c>true</c> se o Material for criada com sucesso; 
        /// caso contrário, <c>false</c> se ocorrer um erro durante a criação.
        /// </returns>
        /// ajustar ainda n ta pronto
        public bool CreateMaterialDB(Material material)
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
                            "INSERT INTO [Material] (MATERIALCODE, MATERIALDESCRIPTION) VALUES (@MATERIALCODE, @MATERIALDESCRIPTION)",
                            Connection,
                            transaction))
                        {
                            cmdOrder.Parameters.AddWithValue("@MATERIALCODE", material.MaterialCode);
                            cmdOrder.Parameters.AddWithValue("@MATERIALDESCRIPTION", material.MaterialDescription);


                            linhasAfetadas = cmdOrder.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        return linhasAfetadas > 0;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao criar Material, verifique os dados.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o Material no banco de dados.", ex);
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
        /// Atualiza os dados de um Material no banco de dados.
        /// </summary>
        /// <param name="Material">Objeto <see cref="Material"/> contendo os novos dados de um Material a serem atualizados.</param>
        /// <returns>
        /// Retorna <c>true</c> se o Material foi atualizada com sucesso; 
        /// caso contrário, <c>false</c> se a atualização falhar.
        /// </returns>
        public bool EditeMaterial(Material material)
        {
            int linhasAfetadas = 0;

            try
            {
                Connection.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE [Material] SET MATERIALDESCRIPTION = @MATERIALDESCRIPTION WHERE [MATERIALCODE] = @MATERIALCODE", Connection))
                {
                    cmd.Parameters.AddWithValue("@MATERIALCODE", material.MaterialCode);
                    cmd.Parameters.AddWithValue("@MATERIALDESCRIPTION", material.MaterialDescription);


                    linhasAfetadas = cmd.ExecuteNonQuery();
                }

                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na edição do Material: {ex.Message}");
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
        /// Remove um Material do banco de dados com base no número da ordem (OS).
        /// </summary>
        /// <param name="MaterialCode">O número identificador único do Material que será removida.</param>
        /// <returns>
        /// Retorna <c>true</c> se o Material foi removido com sucesso; 
        /// caso contrário, <c>false</c> se a remoção falhar.
        /// </returns>
        public bool DeleteMaterial(string MaterialCode)
        {
            int linhasAfetadas = 0;
            try
            {
                Connection.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM [Material] WHERE [MATERIALCODE] = @MATERIALCODE", Connection))
                {
                    cmd.Parameters.AddWithValue("@MATERIALCODE", MaterialCode);

                    linhasAfetadas = cmd.ExecuteNonQuery();
                }
                return linhasAfetadas > 0;
            }

            catch (Exception ex)
            {
                throw new Exception("Erro na deleção do Material.", ex);
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
