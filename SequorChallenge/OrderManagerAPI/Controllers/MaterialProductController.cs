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
    public class MaterialProductController : ControllerBase
    {
        private readonly DALProductMaterial _sql;
        private readonly ILogger<MaterialController> _logger;

        public MaterialProductController(DALProductMaterial sql, ILogger<MaterialController> logger)
        {
            _sql   = sql;
            _logger  = logger;
        }

        [HttpGet]
        [Route("GetProductInMaterial")]
        public IEnumerable<MaterialProduct> GetMaterial(string MaterialCode)
        {
         
            try
            {
                List<MaterialProduct> ListMaterial = _sql.GetListMaterial(MaterialCode);
                return ListMaterial;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de Produtos relacionados aos materiais: {ex.Message}");
                return null;
            }

        }

        [HttpGet]
        [Route("GetMaterialinProduct")]
        public IEnumerable<MaterialProduct> GetProduct(string ProductCode)
        {
            try
            {
                List<MaterialProduct> ListMaterial = _sql.GetListMaterial(null, ProductCode);
                return ListMaterial;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de Material realacionado aos produtos: {ex.Message}");
                return null;
            }

        }

    }
}
