using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALMaterialSQL;
using OrderManagerAPI.DALProductSQL;
using OrderManagerAPI.DALProductMaterialSQL;


namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly DALMaterial _sql;
        private readonly DALProductMaterial _sqlProductMaterial;
        private readonly ILogger<MaterialController> _logger;

        public MaterialController(DALMaterial sql, DALProductMaterial sqlProductMaterial, ILogger<MaterialController> logger)
        {
            _sql                = sql;
            _sqlProductMaterial = sqlProductMaterial;
            _logger             = logger;
        }

        [HttpGet]
        [Route("GetMaterial")]
        public IEnumerable<Material> Get()
        {
            try
            {
                List<Material> ListMaterial = _sql.GetListMaterialDB();
                return ListMaterial;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de Material: {ex.Message}");
                return null;
            }

        }

        [HttpPost]
        [Route("SetMaterial")]
        public IActionResult CreateMaterialt([FromBody] List<Order> newMaterial)
        {
            if (newMaterial == null)
            {
                return BadRequest("Dados inv�lidos para cria��o do Material.");
            }

            try
            {
                var GetIdMaterial = _sql.GetLastMaterial();

                foreach (var item in newMaterial)
                {
                    foreach(var materialAdd in item.Materials)
                    {
                        materialAdd.MaterialCode = GetIdMaterial;
                    }
                }
                             
                 var material = new Material
                {
                    MaterialCode        = newMaterial[0].Materials[0].MaterialCode,
                    MaterialDescription = newMaterial[0].Materials[0].MaterialDescription
                };

                _sql.CreateMaterialDB(material);

                if (newMaterial[0].ProductCode != null && newMaterial[0].ProductCode != "")
                {
                    _sqlProductMaterial.CreateProductMaterial(newMaterial);
                }

                return StatusCode(200, "Material criado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro n�o esperado: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPut("UpdateMaterial")]
        public IActionResult PutProduct([FromBody] List<Order> newMaterial)
        {
            try
            {
                if (newMaterial == null)
                {
                    return BadRequest("Dados inv�lidos para cria��o do Material.");
                }

                if (!_sql.validateMaterialCode(newMaterial[0].Materials[0].MaterialCode))
                {
                    return NotFound("Erro ao validar o c�digo do Material. Verifique o c�digo fornecido.");
                }

                var material = new Material
                {
                    MaterialCode = newMaterial[0].Materials[0].MaterialCode,
                    MaterialDescription = newMaterial[0].Materials[0].MaterialDescription
                };


                _sql.EditeMaterial(material);

                if (newMaterial[0].ProductCode != null && newMaterial[0].ProductCode != "")
                {
                    _sqlProductMaterial.ProcessProduct(newMaterial);
                }
                return Ok("Material alterado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro n�o esperado: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("Delete/{MaterialCode}")]
        public IActionResult DeleteOrdem(string MaterialCode)
        {
            try
            {
                if (!_sql.validateMaterialCode(MaterialCode))
                {
                    return NotFound("Erro ao validar o c�digo do Material. Verifique o c�digo fornecido.");
                }

                _sqlProductMaterial.DeleteProductMaterial(MaterialCode);

                _sql.DeleteMaterial(MaterialCode);

                return Ok("Material exclu�do com sucesso!");

            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                if (errorMessage.Contains("FK__")) 
                {
                    return BadRequest("N�o � poss�vel excluir o material porque ele est� associado a outros Produtos.");
                }
                else
                {
                    Console.WriteLine($"Erro inesperado: {ex.Message}");
                    return StatusCode(500, "Erro interno do servidor.");
                }
            }

        }

    }
}
