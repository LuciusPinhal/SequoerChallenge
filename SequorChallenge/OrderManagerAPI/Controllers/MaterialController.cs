using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALOrderSQL;
using OrderManagerAPI.DALProductSQL;


namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly DALOrder _sql;
        private readonly DALProduct _sqlProduct;
        private readonly ILogger<MaterialController> _logger;

        public MaterialController(DALOrder sql, DALProduct sqlProduct, ILogger<MaterialController> logger)
        {
            _sql = sql;
            _sqlProduct = sqlProduct;
            _logger = logger;
        }

        //Nao finalizada

    }
}
