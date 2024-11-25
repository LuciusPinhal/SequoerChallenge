using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALMaterialSQL;
using OrderManagerAPI.DALProductSQL;


namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly DALMaterial _sql;
        private readonly ILogger<MaterialController> _logger;

        public MaterialController(DALMaterial sql, ILogger<MaterialController> logger)
        {
            _sql = sql;
            _logger = logger;
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
        public IActionResult CreateMaterialt([FromBody] Material newMaterial)
        {
            if (newMaterial == null)
            {
                return BadRequest("Dados inválidos para criação do Material.");
            }

            try
            {
                newMaterial.MaterialCode = _sql.GetLastMaterial();

                var material = new Material
                {
                    MaterialCode        = newMaterial.MaterialCode,
                    MaterialDescription = newMaterial.MaterialDescription
                };

                _sql.CreateMaterialDB(material);

                return Ok("Material criado com sucesso!");
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
