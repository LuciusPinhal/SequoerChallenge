using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALSQl;


namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly DALSQlServer _sql;
        private readonly ILogger<OrderController> _logger;

        public OrderController(DALSQlServer sql, ILogger<OrderController> logger)
        {
            _sql = sql;
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
        [Route("Post")]
        public IActionResult CreateLoja([FromBody] Order newOrder)
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

                _sql.CreateOrder(order);

                return Ok("Ordem criada com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }


        //[HttpPut]
        //[HttpPut("Put")]

        //[HttpDelete]
        //[HttpDelete("Delete/{DeleteId}")]


    }
}




//GetOrders
//    GetProduction
//            SetProduction