using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALOrderSQL;


namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly DALOrder _sql;
        private readonly ILogger<OrderController> _logger;

        public OrderController(DALOrder sql, ILogger<OrderController> logger)
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
        [Route("SetOrder")]
        public IActionResult CreateOrder([FromBody] Order newOrder)
        {
            if (newOrder == null)
            {
                return BadRequest("Dados inv�lidos para cria��o de loja.");
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
                _logger.LogError($"Erro n�o esperado: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }


        [HttpPut]
        [HttpPut("UpdateOrder")]
        public IActionResult PutOrder([FromBody] Order model)
        {
            try
            {
                Order FindOS = _sql.GetOrderDB(model.OS);

                //verificar tbm se o codigo do produto � valido - ainda nn fiz
                if (FindOS != null)
                {
                   
                    _sql.EditeOrder(FindOS);

                    return Ok("Ordem alterada com sucesso");
                    
                }
                else
                {
                    return NotFound("Order n�o encontrada");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro n�o esperado: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        //[HttpDelete]
        //[HttpDelete("Delete/{DeleteId}")]


    }
}




//GetOrders
//GetProduction
//SetProduction