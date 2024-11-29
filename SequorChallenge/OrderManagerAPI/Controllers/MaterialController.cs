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
                return BadRequest("Dados inválidos para criação do Material.");
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

                if (newMaterial[0].ProductCode != null)
                {
                    _sqlProductMaterial.CreateProductMaterial(newMaterial);
                }

                return StatusCode(200, "Material criado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPut("UpdateMaterial")]
        public IActionResult PutProduct([FromBody] Material Material)
        {
            try
            {
                if (Material == null)
                {
                    return BadRequest("Dados inválidos para criação do Material.");
                }

                if (!_sql.validateMaterialCode(Material.MaterialCode))
                {
                    return NotFound("Erro ao validar o código do Material. Verifique o código fornecido.");
                }

                _sql.EditeMaterial(Material);
                return Ok("Material alterado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro não esperado: {ex.Message}");
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
                    return NotFound("Erro ao validar o código do Material. Verifique o código fornecido.");
                }

                _sqlProductMaterial.DeleteProductMaterial(MaterialCode);

                _sql.DeleteMaterial(MaterialCode);

                return Ok("Material excluído com sucesso!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }

        }

    }
}
