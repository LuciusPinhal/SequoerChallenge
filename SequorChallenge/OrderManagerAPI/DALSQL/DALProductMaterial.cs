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

        public bool EditProductMaterial(List<Order> orders)
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
                            using (SqlCommand cmdOrder = new SqlCommand(
                                "UPDATE [ProductMaterial] " +
                                "SET PRODUCTCODE = @NEW_PRODUCTCODE " +
                                "WHERE MATERIALCODE = @MATERIALCODE",
                                Connection,
                                transaction))
                            {
                                cmdOrder.Parameters.AddWithValue("@NEW_PRODUCTCODE", order.ProductCode);

                                foreach (var material in order.Materials)
                                {
                                    cmdOrder.Parameters.AddWithValue("@MATERIALCODE", material.MaterialCode);

                                    try
                                    {
                                        linhasAfetadas += cmdOrder.ExecuteNonQuery();
                                    }
                                    catch (Exception ex)
                                    {
                        
                                        if (ex.Message.Contains("Viola")) 
                                        {
                                    
                                            Console.WriteLine($"Chave duplicada encontrada para o material {material.MaterialCode}. Ignorando este erro.");
                                            continue; 
                                        }
                                        else
                                        {
                                            // Caso seja outro erro, lança a exceção
                                            throw new Exception("Erro ao editar o código do produto em ProductMaterial. Verifique os dados.", ex);
                                        }
                                    }
                                }
                            }
                        }

                        transaction.Commit();
                        return linhasAfetadas > 0;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao editar o código do produto em ProductMaterial. Verifique os dados.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar o Produto no banco de dados.", ex);
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
        }

        public bool EditMaterialProduct(List<Order> orders)
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
                                    "UPDATE [ProductMaterial] " +
                                    "SET MATERIALCODE = @NEW_MATERIALCODE " +
                                    "WHERE PRODUCTCODE = @PRODUCTCODE",
                                    Connection,
                                    transaction))
                                {
                                    // Novo código do material
                                    cmdOrder.Parameters.AddWithValue("@NEW_MATERIALCODE", material.MaterialCode);
                                    // Código do produto usado como critério
                                    cmdOrder.Parameters.AddWithValue("@PRODUCTCODE", order.ProductCode);

                                    try
                                    {
                                        linhasAfetadas += cmdOrder.ExecuteNonQuery();
                                    }
                                    catch (Exception ex)
                                    {                            
                                        if (ex.Message.Contains("Viola"))  
                                        {                                 
                                            Console.WriteLine($"Chave duplicada encontrada para o material {material.MaterialCode}. Ignorando este erro.");
                                            continue; 
                                        }
                                        else
                                        {
                                            throw new Exception("Erro ao editar o código do material em ProductMaterial. Verifique os dados.", ex);
                                        }
                                    }
                                }
                            }
                        }

                        transaction.Commit();
                        return linhasAfetadas > 0;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao editar o código do material em ProductMaterial. Verifique os dados.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar o Material no banco de dados.", ex);
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

        public List<MaterialProduct> GetListMaterial(string? MaterialCode = null, string? ProductCode = null)
        {
            var listProduct = new List<MaterialProduct>();

            try
            {
                Connection.Open();

               string query = ProductCode == null
                   ? "SELECT * FROM [ProductMaterial] WHERE MaterialCode = @MATERIALCODE"
                   : "SELECT * FROM [ProductMaterial] WHERE ProductCode = @PRODUCTCODE"; 

                using (var cmd = new SqlCommand(query, Connection))
                {
   
                    if (!string.IsNullOrEmpty(MaterialCode))
                    {
                        cmd.Parameters.AddWithValue("@MATERIALCODE", MaterialCode);
                    }

                    if (!string.IsNullOrEmpty(ProductCode))
                    {
                        cmd.Parameters.AddWithValue("@PRODUCTCODE", ProductCode);
                    }

                    // Executar o comando e ler os resultados
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var listMaterial = new MaterialProduct
                            {
                                ProductCode = reader.GetString(0),  // Ajuste o índice se necessário
                                MaterialCode = reader.GetString(1), // Ajuste o índice se necessário
                            };

                            listProduct.Add(listMaterial);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar Materiais e Produtos relacionados. Verifique o cadastro no banco de dados", ex);
            }
            finally
            {
                if (Connection.State == System.Data.ConnectionState.Open)
                {
                    Connection.Close();
                }
            }

            return listProduct;
        }



    }
}
