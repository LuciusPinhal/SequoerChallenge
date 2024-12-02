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

        private List<MaterialProduct> ListMaterialrProduct = new List<MaterialProduct>();
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

        public void ProcessMateriais(List<Order> orders)
        {
            var materiaisIguais = new List<Material>();
            var materiaisParaAlterar = new List<Material>();
            var listMaterialProduct = new List<MaterialProduct>();
            int quantidadeMateriaisNaOrder = 0;

            foreach (var order in orders)
            {
                // Obtenha a lista de materiais associada ao código do produto da ordem
                listMaterialProduct = GetListMaterial(null, order.ProductCode);
                quantidadeMateriaisNaOrder = order.Materials.Count;

                foreach (var materialOrder in order.Materials)
                {
                    // Verificar se o material da ordem está na lista da base
                    if (listMaterialProduct.Any(m => m.MaterialCode == materialOrder.MaterialCode))
                    {
                        // Adicionar materiais iguais à lista
                        materiaisIguais.Add(materialOrder);
                    }
                    else
                    {
                        // Adicionar materiais diferentes (novos) à lista de alteração
                        materiaisParaAlterar.Add(materialOrder);
                    }
                }
            }
            int result = quantidadeMateriaisNaOrder - listMaterialProduct.Count;

            //Alter table Normal
            if (result == 0)
            {                  
                EditMaterialProduct(materiaisParaAlterar, orders[0].ProductCode);
                
            }
            //alter table + delete                   
            if (result < 0)
            {
                var order = new Order
                {
                    OS = "",
                    Quantity = 0,
                    ProductCode = orders[0].ProductCode,
                    ProductDescription = "",
                    Image = "",
                    CycleTime = 0,
                    Materials = new List<Material>()
                };

                if (materiaisParaAlterar.Count > 0)
                {
                    foreach (var material in materiaisParaAlterar)
                    {
                        order.Materials.Add(new Material
                        {
                            MaterialCode = material.MaterialCode,
                            MaterialDescription = material.MaterialDescription
                        });
                    }

                }

                if (materiaisIguais.Count > 0)
                {
                    foreach (var material in materiaisIguais)
                    {
                        order.Materials.Add(new Material
                        {
                            MaterialCode = material.MaterialCode,
                            MaterialDescription = material.MaterialDescription
                        });
                    }

                }

                foreach (var deleteMaterial in listMaterialProduct)
                {
                    DeleteMaterial(deleteMaterial.MaterialCode, deleteMaterial.ProductCode);
                }

                if (orders[0].Materials != null)
                {
                    CreateProductMaterial(new List<Order> { order });
                }
                

            }
            //alter table + Create
            else if (result > 0)
            {
                //n faz nd 
                if (materiaisIguais.Count == 0)
                {
                    foreach (var deleteMaterial in listMaterialProduct)
                    {
                        DeleteMaterial(deleteMaterial.MaterialCode, deleteMaterial.ProductCode);
                    }
                }

                if (materiaisParaAlterar.Count > 0)
                {
                    var order = new Order
                    {
                        OS = "",
                        Quantity = 0,
                        ProductCode = orders[0].ProductCode,
                        ProductDescription = "",
                        Image = "",
                        CycleTime = 0,
                        Materials = new List<Material>()
                    };

                    if (materiaisParaAlterar.Count > 0)
                    {
                        foreach (var material in materiaisParaAlterar)
                        {
                            order.Materials.Add(new Material
                            {
                                MaterialCode = material.MaterialCode,
                                MaterialDescription = material.MaterialDescription
                            });
                        }

                    }
                    if (orders[0].Materials != null)
                    {
                        CreateProductMaterial(new List<Order> { order });
                    }                                
                }
            }
            else
            {
                throw new Exception("Erro ao editar o código do material em ProductMaterial. Verifique os dados.");
            }
        }


        public void ProcessProduct(List<Order> orders)
        {
            var ProdutosIguais = new List<Order>();
            var ProdutosParaAlterar = new List<Order>();
            var listMaterialProduct = new List<MaterialProduct>();
            int quantidadeMateriaisNaOrder = 0;
            var Materialcode = orders[0].Materials[0].MaterialCode;

            foreach (var order in orders)
            {
                // Obtenha a lista de materiais associada ao código do produto da ordem
                listMaterialProduct = GetListMaterial(Materialcode, null);
                quantidadeMateriaisNaOrder = order.Materials.Count;

                    if (listMaterialProduct.Any(m => m.ProductCode == order.ProductCode))
                    {
  
                        ProdutosIguais.Add(order);
                    }
                    else
                    {
                        ProdutosParaAlterar.Add(order);
                    }
            }
            int result = orders.Count - listMaterialProduct.Count;

         
            //Alter table Normal
            if (result == 0)
            {
                EditProductMaterial(ProdutosParaAlterar, Materialcode);
              
            }
            //alter table + delete                   
            if (result < 0)
            {
                List<Order> list = new List<Order>();

                if (ProdutosParaAlterar.Count > 0)
                {
                    foreach (var product in ProdutosParaAlterar)
                    {
                        var order = new Order
                        {
                            OS = "",
                            Quantity = 0,
                            ProductCode = product.ProductCode,
                            ProductDescription = "",
                            Image = "",
                            CycleTime = 0,
                            Materials = new List<Material>()
                            {
                                new Material()
                                {
                                    MaterialCode = Materialcode,
                                    MaterialDescription= "",
                                   
                                }
                            }
                        };

                        list.Add(order);
                    }

                }

                if (ProdutosIguais.Count > 0)
                {
                    foreach (var product in ProdutosIguais)
                    {
                        var order = new Order
                        {
                            OS = "",
                            Quantity = 0,
                            ProductCode = product.ProductCode,
                            ProductDescription = "",
                            Image = "",
                            CycleTime = 0,
                            Materials = new List<Material>()
                            {
                                new Material()
                                {
                                    MaterialCode = Materialcode,
                                    MaterialDescription= "",

                                }
                            }
                        };

                        list.Add(order);
                    }

                }

                foreach (var deleteMaterial in listMaterialProduct)
                {
                    DeleteMaterial(deleteMaterial.MaterialCode, deleteMaterial.ProductCode);
                }

                if (list.Count > 0)
                {
                    CreateProductMaterial(list);
                }                       

            }
            //alter table + Create
            else if (result > 0)
            {

                //n faz nd 
                if (ProdutosIguais.Count == 0)
                {
                    foreach (var deleteMaterial in listMaterialProduct)
                    {
                        DeleteMaterial(deleteMaterial.MaterialCode, deleteMaterial.ProductCode);
                    }
                }

                List<Order> list = new List<Order>();

                if (ProdutosParaAlterar.Count > 0)
                {
                    foreach (var product in ProdutosParaAlterar)
                    {
                        var order = new Order
                        {
                            OS = "",
                            Quantity = 0,
                            ProductCode = product.ProductCode,
                            ProductDescription = "",
                            Image = "",
                            CycleTime = 0,
                            Materials = new List<Material>()
                            {
                                new Material()
                                {
                                    MaterialCode = Materialcode,
                                    MaterialDescription= "",

                                }
                            }
                        };

                        list.Add(order);

                        if (list.Count > 0)
                        {
                            CreateProductMaterial(list);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Erro ao editar o código do material em ProductMaterial. Verifique os dados.");
            }
        }

        private bool EditMaterialProduct(List<Material> Materials, string ProductCode)
        {
            int linhasAfetadas = 0;

            //procuct P001 - M001 - M002

            try
            {
                Connection.Open();

                using (var transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var material in Materials)
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
                                cmdOrder.Parameters.AddWithValue("@PRODUCTCODE", ProductCode);

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

        private bool EditProductMaterial(List<Order> ProductsCode, string MaterialCode)
        {
            int linhasAfetadas = 0;
            try
            {
                Connection.Open();

                using (var transaction = Connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var product in ProductsCode)
                        {
                            using (SqlCommand cmdOrder = new SqlCommand(
                                "UPDATE [ProductMaterial] " +
                                "SET PRODUCTCODE = @NEW_PRODUCTCODE " +
                                "WHERE MATERIALCODE = @MATERIALCODE",
                                Connection,
                                transaction))
                            {
                                // Novo código do material
                                cmdOrder.Parameters.AddWithValue("@NEW_PRODUCTCODE", product.ProductCode);
                                // Código do produto usado como critério
                                cmdOrder.Parameters.AddWithValue("@MATERIALCODE", MaterialCode);

                                try
                                {
                                    linhasAfetadas += cmdOrder.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    if (ex.Message.Contains("Viola"))
                                    {
                                        Console.WriteLine($"Chave duplicada encontrada para o product {product.ProductCode}. Ignorando este erro.");
                                        continue;
                                    }
                                    else
                                    {
                                        throw new Exception("Erro ao editar o código do product em ProductMaterial. Verifique os dados.", ex);
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
                        throw new Exception("Erro ao editar o código do Product em ProductMaterial. Verifique os dados.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao editar o Product no banco de dados.", ex);
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
                    if (ProductCode != null)
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

        public bool DeleteMaterial(string MaterialCode, string ProductCode)
        {
            int linhasAfetadas = 0;
            try
            {
                Connection.Open();
                string query = "DELETE FROM [ProductMaterial] WHERE [ProductCode] = @ProductCode AND [MaterialCode] = @MaterialCode";

                using (SqlCommand cmd = new SqlCommand(query, Connection))
                {
                    cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
                    cmd.Parameters.AddWithValue("@MaterialCode", MaterialCode);

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
