using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.Models;
using OrderManagerAPI.DALBaseSQL;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;

namespace OrderManagerAPI.DALProductMaterialSQL
{
    public class DALProductMaterial : DALBase
    {
        public DALProductMaterial(IConfiguration configuration) : base(configuration) { }

        public bool CreateProductMaterial(List<Order> orders)
        {
            int linhasAfetadas = 0;

            try
            {
                Connection.Open();

                using (var transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var order in orders) 
                        {
                            foreach (var material in order.Materials) 
                            {
                                using (SqlCommand cmdOrder = new SqlCommand(
                                    "INSERT INTO [ProductMaterial] (PRODUCTCODE, MATERIALCODE) VALUES (@PRODUCTCODE, @MATERIALCODE)",
                                    Connection,
                                    transaction))
                                {
                                    cmdOrder.Parameters.AddWithValue("@PRODUCTCODE", order.ProductCode);
                                    cmdOrder.Parameters.AddWithValue("@MATERIALCODE", material.MaterialCode);

                                    linhasAfetadas += cmdOrder.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();

                        return linhasAfetadas > 0;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao criar o relacionamento Produto-Material, verifique os dados.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar o Produto-Material no banco de dados.", ex);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        public bool DeleteProductMaterial(string? MaterialCode = null, string? ProductCode = null)
        {
            int linhasAfetadas = 0;
            try
            {              
                Connection.Open();
                string query = ProductCode != null
                    ? "DELETE FROM [ProductMaterial] WHERE [ProductCode] = @ProductCode"
                    : "DELETE FROM [ProductMaterial] WHERE [MATERIALCODE] = @MATERIALCODE";

                using (SqlCommand cmd = new SqlCommand(query, Connection))
                {
                    if(ProductCode != null)
                    {
                        cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MATERIALCODE", MaterialCode);
                    }

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
