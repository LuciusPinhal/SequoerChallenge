using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderManagerAPI.Models;
using OrderManagerAPI.DALUserSQL;
using OrderManagerAPI.DALOrderSQL;
using OrderManagerAPI.DALProductSQL;
using OrderManagerAPI.DALMaterialSQL;
using OrderManagerAPI.DALProductionSQL;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;

//boa sorte lucius do futuro pq eu n sei essa merda :)

namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionController : ControllerBase
    {

        private readonly DALUser _sqlUser;
        private readonly DALOrder _sqlOrder;
        private readonly DALProduction _sql;
        private readonly DALProduct _sqlProduct;
        private readonly DALMaterial _sqlMaterial;
        private readonly ILogger<ProductionController> _logger;

        public ProductionController(DALProduction sql, DALUser sqlUser, DALMaterial sqlMaterial, DALOrder sqlOrder, DALProduct sqlProduct, ILogger<ProductionController> logger)
        {
            _sql         = sql;
            _sqlUser     = sqlUser;
            _sqlOrder    = sqlOrder;
            _sqlProduct  = sqlProduct;
            _sqlMaterial = sqlMaterial;
            _logger      = logger;
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

        [HttpPost]
        [Route("SetProduction")]
        public IActionResult SetProduction([FromBody] Production newProduction)
        {
            try
            {           
                var validationResult = ValidateProduction(newProduction);
                if (validationResult is BadRequestObjectResult badRequestResult)
                {
                    return badRequestResult;
                }

                _sql.CreateOrderDB(newProduction);

                if (validationResult is OkObjectResult okResult)
                {
                    return okResult;
                }

                return Ok("Produção Criado com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }


        [HttpPut("UpdateProduction")]
        public IActionResult PutProduction([FromBody] Production newProduction)
        {

            try
            {
                var validationResult = ValidateProduction(newProduction);
                if (validationResult is BadRequestObjectResult badRequestResult)
                {
                    return badRequestResult; 
                }
      
                _sql.EditeProduction(newProduction);
                
                if (validationResult is OkObjectResult okResult)
                {
                    return okResult; 
                }
               
                return Ok("Produção alterada com sucesso!");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Valida os dados de uma nova produção e retorna o resultado da validação.
        /// </summary>
        /// <param name="newProduction">Objeto da nova produção a ser validada.</param>
        /// <returns>Um IActionResult contendo os erros ou mensagens de sucesso da validação.</returns>
        private IActionResult ValidateProduction(Production newProduction)
        {
            if (newProduction == null)
            {
                return BadRequest(new { errors = new List<string> { "Ordem de Produção não encontrada." } });
            }

            var validationErrors = new List<string>();
            var infoMessages = new List<string>();

            ValidateEmail(newProduction.Email, validationErrors);
            Order order = ValidateOrder(newProduction.Order, validationErrors);

            if (order != null)
            {
                ValidateProductionDate(newProduction.ProductionDate, validationErrors);
                ValidateQuantity(newProduction, order, validationErrors, infoMessages);
                ValidateMaterialCode(newProduction.materialCode, validationErrors);
                ValidateCycleTime(newProduction, order, newProduction.CycleTime, validationErrors, infoMessages);
            }

            if (validationErrors.Any())
            {
                return BadRequest(new { errors = validationErrors, info = infoMessages });
            }

            if (infoMessages.Any())
            {
                return Ok(new { message = "Produção validada com sucesso, mas com informações.", info = infoMessages });
            }

            return Ok(new { message = "Produção validada com sucesso." });
        }

        /// <summary>
        /// Valida o e-mail do usuário associado à produção.
        /// </summary>
        /// <param name="email">E-mail do usuário a ser validado.</param>
        /// <param name="validationErrors">Lista de erros de validação para preenchimento.</param>
        private void ValidateEmail(string email, List<string> validationErrors)
        {
            if (!_sqlUser.validateEmailUser(email))
            {
                validationErrors.Add("Erro ao validar o Email do Usuario. Verifique o Email fornecido.");
            }
        }

        /// <summary>
        /// Valida se a ordem existe no banco de dados.
        /// </summary>
        /// <param name="orderNumber">Número da ordem a ser validado.</param>
        /// <param name="validationErrors">Lista de erros de validação para preenchimento.</param>
        /// <returns>Objeto Order se encontrado, ou null caso contrário.</returns>
        private Order ValidateOrder(string orderNumber, List<string> validationErrors)
        {
            var order = _sqlOrder.GetOrderDB(orderNumber);

            if (order == null)
            {
                validationErrors.Add("Erro ao encontrar a Ordem. Verifique se o número da Ordem está correto.");
            }

            return order;
        }

        /// <summary>
        /// Valida a data de produção, caso fornecida.
        /// </summary>
        /// <param name="productionDate">Data da produção a ser validada.</param>
        /// <param name="validationErrors">Lista de erros de validação para preenchimento.</param>
        private void ValidateProductionDate(string productionDate, List<string> validationErrors)
        {
            if (!string.IsNullOrEmpty(productionDate))
            {
                // Adicione a lógica de validação para a data, se necessário
            }
        }

        /// <summary>
        /// Valida a quantidade de produção e compara com a quantidade permitida pela ordem.
        /// </summary>
        /// <param name="newProduction">Objeto Production com os dados da nova produção.</param>
        /// <param name="order">Objeto Order associado à produção.</param>
        /// <param name="validationErrors">Lista de erros de validação para preenchimento.</param>
        /// <param name="infoMessages">Lista de mensagens informativas para preenchimento.</param>
        private void ValidateQuantity(Production newProduction, Order order, List<string> validationErrors, List<string> infoMessages)
        {
            if (newProduction.Quantity <= 0)
            {
                validationErrors.Add("Quantidade tem que ser superior a 0.");
            }

            if (newProduction.Quantity > order.Quantity)
            {
                validationErrors.Add($"Quantidade tem que ser menor ou igual à quantidade da Ordem selecionada. Quantidade da Ordem: {order.Quantity}");
            }

            if (newProduction.Id > 0)
            {
                var oldProduction = _sql.FindProduction(newProduction.Id);

                if (oldProduction.Quantity > newProduction.Quantity)
                {
                    infoMessages.Add($"A quantidade anterior era maior que a quantidade atual. Quantidade anterior: {oldProduction.Quantity}");
                }
            }
        }

        /// <summary>
        /// Valida o código do material fornecido na produção.
        /// </summary>
        /// <param name="materialCode">Código do material a ser validado.</param>
        /// <param name="validationErrors">Lista de erros de validação para preenchimento.</param>
        private void ValidateMaterialCode(string materialCode, List<string> validationErrors)
        {
            if (!_sqlMaterial.validateMaterialCode(materialCode))
            {
                validationErrors.Add("Erro ao validar o código do Material. Verifique o Código do Material fornecido.");
            }
        }

        /// <summary>
        /// Valida o tempo de ciclo da produção.
        /// </summary>
        /// <param name="cycleTime">Tempo de ciclo da produção a ser validado.</param>
        /// <param name="validationErrors">Lista de erros de validação para preenchimento.</param>
        private void ValidateCycleTime(Production newProduction, Order order, double cycleTime, List<string> validationErrors, List<string> infoMessages)
        {
            order = _sqlProduct.GetProdutoDB(order.ProductCode);

            if (order == null)
            {
                validationErrors.Add("Erro ao carregar informações do produto com o código fornecido.");
                
            } 
            else
            {
                if (cycleTime <= 0)
                {
                    validationErrors.Add("O tempo de ciclo tem que ser superior a 0.");
                }
                if (newProduction.CycleTime > order.CycleTime)
                {
                    infoMessages.Add($"O tempo de ciclo informado é superior ao tempo de ciclo estimado para o produto. Tempo informado na OS: {order.CycleTime}");
                }
            }

        }



        //Production FindProduction = _sql.FindProduction(newProduction);

        //[HttpDelete]
        //[HttpDelete("Delete/{DeleteId}")]
    }
}

