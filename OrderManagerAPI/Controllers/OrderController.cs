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


        [HttpGet]
        [Route("GetOrders")]
        public IEnumerable<Order> Get()
        {
            try
            {
                var lojas = _sql.GetOrdersDB();
                //return lojas;
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de lojas: {ex.Message}");
                return null;
            }

        }

        //[HttpPost]
        //[Route("Post")]


        //[HttpPut]
        //[HttpPut("Put")]

        //[HttpDelete]
        //[HttpDelete("Delete/{DeleteId}")]


    }
}




//GetOrders
//    GetProduction
//            SetProduction