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
        /// Metodo Get para pegar lista Produção
        /// </summary>
        /// <param name="email">Email Cliente</param>
        /// <returns>Lista da Produção</returns>
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
                _logger.LogError($"Erro ao obter a lista de produção: {ex.Message}");
                return null;
            }

        }

        //[HttpPost]
        //[Route("SetProduction")]
        //public IActionResult CreateOrder([FromBody] Order newOrder)
        //{
        //    if (newOrder == null)
        //    {
        //        return BadRequest("Dados inválidos para criação de loja.");
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
        //        _logger.LogError($"Erro não esperado: {ex.Message}");
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