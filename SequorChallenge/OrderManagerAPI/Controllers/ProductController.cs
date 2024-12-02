using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALOrderSQL;
using OrderManagerAPI.DALProductSQL;
using OrderManagerAPI.DALProductMaterialSQL;


namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DALProduct _sql;
        private readonly DALProductMaterial _sqlProductMaterial;
        private readonly ILogger<ProductController> _logger;

        public ProductController(DALProduct sql, DALProductMaterial sqlProductMaterial, ILogger<ProductController> logger)
        {
            _sql                = sql;
            _sqlProductMaterial = sqlProductMaterial;
            _logger             = logger;
        }

        [HttpGet]
        [Route("GetProduct")]
        public IEnumerable<Order> Get()
        {
            try
            {
                List<Order> ListOrders = _sql.GetListProdutoDB();
                return ListOrders;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de Ordem: {ex.Message}");
                return null;
            }

        }

        [HttpPost]
        [Route("SetProduct")]
        public IActionResult CreateProduct([FromBody] Order newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest("Dados inválidos para criação do Produto.");
            }
            if (newOrder.CycleTime <= 0)
            {
                return BadRequest("O tempo de ciclo tem que ser superior a 0.");
            }

            try
            {
                newOrder.ProductCode = _sql.GetLastProduct();

                var order = new Order
                {                      
                    ProductCode = newOrder.ProductCode,
                    ProductDescription = newOrder.ProductDescription,
                    Image = newOrder.Image,
                    CycleTime = newOrder.CycleTime,
                    Materials = newOrder.Materials.ToList()

                };

                _sql.CreateProductDB(order);

                if (order?.Materials != null && order.Materials.Any())
                {
                    _sqlProductMaterial.CreateProductMaterial(new List<Order> { order });
                }
              
                return Ok("Produto criado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPut("UpdateProduct")]
        public IActionResult PutProduct([FromBody] Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Dados inválidos para criação do Produto.");
                }
                if (order.CycleTime <= 0)
                {
                    return BadRequest("O tempo de ciclo tem que ser superior a 0.");
                }

                if (!_sql.validateCodeProduct(order.ProductCode))
                {
                    return NotFound("Erro ao validar o código do produto. Verifique o código fornecido.");
                }

                _sql.EditeProduct(order);

                if (order?.Materials != null && order.Materials.Any())
                {
                    _sqlProductMaterial.ProcessMateriais(new List<Order> { order });
                }

                return Ok("Produto alterado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("Delete/{ProductCode}")]
        public IActionResult DeleteOrdem(string ProductCode)
        {
            try
            {
                if (!_sql.validateCodeProduct(ProductCode))
                {
                    return BadRequest("Erro ao validar o código do produto. Verifique o código fornecido.");
                }

                _sqlProductMaterial.DeleteProductMaterial(null, ProductCode);

                _sql.DeleteProduct(ProductCode);

                return Ok("Produto excluído com sucesso!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }

        }
    }
}
