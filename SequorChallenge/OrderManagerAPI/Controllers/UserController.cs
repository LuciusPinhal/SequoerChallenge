using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALOrderSQL;
using OrderManagerAPI.DALProductSQL;


namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DALOrder _sql;
        private readonly DALProduct _sqlProduct;
        private readonly ILogger<UserController> _logger;

        public UserController(DALOrder sql, DALProduct sqlProduct, ILogger<UserController> logger)
        {
            _sql = sql;
            _sqlProduct = sqlProduct;
            _logger = logger;
        }

        //Nao finalizada

    }
}
