using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALSQl;


namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionController : ControllerBase
    {

        private readonly DALSQlServer _sql;
        private readonly ILogger<ProductionController> _logger;

        public ProductionController(DALSQlServer sql, ILogger<ProductionController> logger)
        {
            _sql = sql;
            _logger = logger;
        }

        /// <summary>
        /// Metodo Get para pegar lista Produ��o
        /// </summary>
        /// <param name="email">Email Cliente</param>
        /// <returns>Lista da Produ��o</returns>
        [HttpGet]
        [Route("GetProduction")]
        public IEnumerable<Production> Get([FromQuery] string email)
        {
            try
            {
                var ListProductions = _sql.GetProductionDB(email);
                return ListProductions;
            
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de produ��o: {ex.Message}");
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



//    GetProduction
//            SetProduction