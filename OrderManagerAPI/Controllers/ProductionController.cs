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


        [HttpGet]
        [Route("GetProduction")]
        public IEnumerable<Production> Get()
        {
            try
            {
                var ListProductions = _sql.GetProductionDB();
                return ListProductions;
            
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de produção: {ex.Message}");
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