using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALOrderSQL;
using OrderManagerAPI.DALProductSQL;


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

        /// <summary>
        /// Metodo Get para Lista de O.S
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <returns>Retorna a lista de O.S </returns>
        [HttpGet]
        [Route("GetOrders")]
        public IEnumerable<Order> Get([FromQuery] string email)
        {
            try
            {
                var ListOrders = _sql.GetOrdersDB(email);
                return ListOrders;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de lojas: {ex.Message}");
                return null;
            }

        }

        /// <summary>
        /// Metodo Post para Criar O.S
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns>Cria O.S</returns>
        [HttpPost]
        [Route("SetOrder")]
        public IActionResult CreateOrder([FromBody] Order newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest("Dados inválidos para criação de loja.");
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
                Order FindOS = _sql.GetOrderDB(order.OS);

                if (FindOS == null)
                {
                    return NotFound("Order não encontrada, verifique o número!");
                }

                if (!_sqlProduct.ValidCodeProduct(order.ProductCode))
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
        public IActionResult DeleteLoja(string Order)
        {
            try
            {
                Order FindOS = _sql.GetOrderDB(Order);

                if (FindOS == null)
                {
                    return NotFound("Order não encontrada, verifique o número!");
                }

                _sql.DeleteOrder(Order);

                return Ok("Ordem excluída com sucesso!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }

        }

    }
}
