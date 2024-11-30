using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALOrderSQL;
using OrderManagerAPI.DALProductSQL;
using System.Data.SqlClient;

namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DALOrder _sql;
        private readonly DALProduct _sqlProduct;
        private readonly ILogger<OrderController> _logger;

        public OrderController(DALOrder sql, DALProduct sqlProduct, ILogger<OrderController> logger)
        {
            _sql = sql;
            _sqlProduct = sqlProduct;
            _logger = logger;
        }

        //[HttpGet]
        //[Route("GetOrders")]
        //public IEnumerable<Order> Get()
        //{
        //    try
        //    {
        //        List<Order> ListOrders = _sql.GetOrdersDB();
        //        return ListOrders;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Erro ao obter a lista de Ordem: {ex.Message}");
        //        return null;
        //    }

        //}

        [HttpGet]
        [Route("GetOrder")]
        public IEnumerable<Order> GetOS()
        {
            try
            {
                List<Order> ListOrders = _sql.GetOSDB();
                return ListOrders;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de Ordem: {ex.Message}");
                return null;
            }

        }

        [HttpPost]
        [Route("SetOrder")]
        public IActionResult CreateOrder([FromBody] Order newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest("Dados inválidos para criação de Ordem.");
            }

            try
            {      
                newOrder.OS = _sql.GetLastOS();

                var order = new Order
                {
                    OS = newOrder.OS,
                    Quantity = newOrder.Quantity,
                    ProductCode = newOrder.ProductCode,
                    ProductDescription = newOrder.ProductDescription,
                    Image = newOrder.Image,
                    CycleTime = newOrder.CycleTime,
                    Materials = new List<Material>()
                };

                _sql.CreateOrderDB(order);

                return Ok("Ordem criada com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPut("UpdateOrder")]
        public IActionResult PutOrder([FromBody] Order order)
        {
            try
            {
                if (!_sql.VerifyOrderDB(order.OS))
                {
                    return NotFound("Order não encontrada, verifique o número!");
                }

                if (!_sqlProduct.validateCodeProduct(order.ProductCode))
                {
                    return BadRequest("Erro ao validar o código do produto. Verifique o código fornecido.");
                }

                _sql.EditeOrder(order);
                return Ok("Ordem alterada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpDelete("Delete/{Order}")]
        public IActionResult DeleteOrdem(string Order)
        {
            try
            {
                if (!_sql.VerifyOrderDB(Order))
                {
                    return NotFound("Order não encontrada, verifique o número!");
                }

                _sql.DeleteOrder(Order);

                return Ok("Ordem excluída com sucesso!");

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    
                    if (ex.InnerException.Message.Contains("FK__Productio__Order__440B1D61"))
                    {
                        return StatusCode(400, "Não é possível excluir o registro, pois existem pendências na tabela Produção.");
                    }
                }

                // Caso contrário, loga o erro e retorna uma mensagem genérica de erro interno
                Console.WriteLine($"Erro não esperado: {ex.Message} {ex.InnerException}");
                return StatusCode(500, "Erro interno do servidor");
            }

        }

    }
}
