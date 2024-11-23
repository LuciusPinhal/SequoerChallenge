using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderManagerAPI.Models;
using OrderManagerAPI.DALProductionSQL;
using System;
using System.Collections.Generic;

//boa sorte lucius do futuro pq eu n sei essa merda :)

namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionController : ControllerBase
    {

        private readonly DALProduction _sql;
        private readonly ILogger<ProductionController> _logger;

        public ProductionController(DALProduction sql, ILogger<ProductionController> logger)
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
        //[Route("SetProduction")]
        //public IActionResult CreateOrder([FromBody] Order newOrder)
        //{
        //    if (newOrder == null)
        //    {
        //        return BadRequest("Dados inv�lidos para cria��o de loja.");
        //    }

        //    try
        //    {
        //        newOrder.OS = _sql.GetLastOS();

        //        var order = new Order
        //        {
        //            OS = newOrder.OS,
        //            Quantity = newOrder.Quantity,
        //            ProductCode = newOrder.ProductCode,
        //            ProductDescription = newOrder.ProductDescription,
        //            Image = newOrder.Image,
        //            CycleTime = newOrder.CycleTime,
        //            Materials = new List<Material>()
        //        };

        //        _sql.CreateOrderDB(order);

        //        return Ok("Ordem criada com sucesso!");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Erro n�o esperado: {ex.Message}");
        //        return StatusCode(500, "Erro interno no servidor");
        //    }
        //}


        //[HttpPut]
        //[HttpPut("Put")]

        //[HttpDelete]
        //[HttpDelete("Delete/{DeleteId}")]
    }
}



//    GetProduction
//            SetProduction