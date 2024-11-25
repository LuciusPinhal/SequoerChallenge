using Microsoft.AspNetCore.Mvc;
using OrderManagerAPI.Models;
using Microsoft.Extensions.Configuration;
using OrderManagerAPI.DALUserSQL;
using OrderManagerAPI.DALProductSQL;
using System.Text.RegularExpressions;

namespace OrderManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DALUser _sql;
        private readonly ILogger<UserController> _logger;

        public UserController(DALUser sql, ILogger<UserController> logger)
        {
            _sql = sql;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetUser")]
        public IEnumerable<User> Get()
        {
            try
            {
                List<User> ListUser = _sql.GetListUserDB();
                return ListUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter a lista de Ordem: {ex.Message}");
                return null;
            }

        }

        [HttpPost]
        [Route("SetUser")]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            try
            {
                var validationResult = ValidateUser(newUser, true);
                if (validationResult is BadRequestObjectResult badRequestResult)
                {
                    return badRequestResult;
                }

                _sql.CreateUserDB(newUser);

                if (validationResult is OkObjectResult okResult)
                {
                    return okResult;
                }

                return Ok("Usuario criado com sucesso!");
       
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro não esperado: {ex.Message}");
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        /// <summary>
        /// Valida os dados de uma nova usuario e retorna o resultado da validação.
        /// </summary>
        /// <param name="newUser">Objeto da novo usuario a ser validado.</param>
        /// <returns>Um IActionResult contendo os erros ou mensagens de sucesso da validação.</returns>
        private IActionResult ValidateUser(User newUser, bool Create)
        {
            if (newUser == null)
            {
                return BadRequest(new { errors = new List<string> { "usuário não encontrada." } });
            }

            var validationErrors = new List<string>();
            var infoMessages = new List<string>();


            if (Create && _sql.validateEmailUser(newUser.Email))
            {
                validationErrors.Add("Erro: Já existe usuário com este Email");
            }
            else
            {
                ValidateUserDate(newUser, Create, validationErrors, infoMessages);
                ValidateUserEmail(newUser.Email, validationErrors);
            }


            if (validationErrors.Any())
            {
                return BadRequest(new { errors = validationErrors, info = infoMessages });
            }

            if (infoMessages.Any())
            {
                return Ok(new { message = "usuário validado com sucesso, mas com informações.", info = infoMessages });
            }

            return Ok(new { message = "usuário validado com sucesso." });
        }


        /// <summary>
        /// Valida o e-mail do usuário.
        /// </summary>
        /// <param name="email">E-mail do usuário a ser validado.</param>
        /// <param name="validationErrors">Lista de erros de validação para preenchimento.</param>
        private User ValidateEmail(string email, List<string> validationErrors)
        {
            User newUser = _sql.FindUser(email);
            if (newUser.Email == null)
            {
                validationErrors.Add("Erro ao validar o Email do Usuario. Verifique o Email fornecido.");
            }
            return newUser;
        }

        /// <summary>
        /// Valida a data do usuario, caso fornecida.
        /// </summary>
        /// <param name="productionDate">Data da produção a ser validada.</param>
        /// <param name="validationErrors">Lista de erros de validação para preenchimento.</param>
        private void ValidateUserDate(User user, bool Create, List<string> validationErrors, List<string> infoMessages)
        {

            if (user.InitialDate == null)
            {
                validationErrors.Add("Erro: A data inicial está vazia ou nula.");
                return;
            }
            if (user.EndDate == null)
            {
                validationErrors.Add("Erro: A data final está vazia ou nula.");
                return;
            }

            if (user.InitialDate < DateTime.Now)
            {
                infoMessages.Add($"Data {user.InitialDate} é inferior a data atual");
            }

            if (user.EndDate < DateTime.Now)
            {
                validationErrors.Add($"Data Final: {user.EndDate} não pode ser inferior a data atual");
            }

            if (!Create)
            {    
                User FindOldUser = ValidateEmail(user.Email, validationErrors);
                if (FindOldUser != null)
                {
                    
                    if (user.InitialDate < FindOldUser.InitialDate)
                    {
                        validationErrors.Add($"Data alterada {user.InitialDate} é inferior à data lançada anteriormente: {FindOldUser.InitialDate} ");
                    }

                    if(user.EndDate > FindOldUser.EndDate)
                    {
                        infoMessages.Add($"Data final {user.EndDate} é maior que a data cadastrada: {FindOldUser.EndDate}");
                    }
                }
            }
        }

        private void ValidateUserEmail(string email, List<string> validationErrors)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.(com|com\.br)$";

            if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, pattern))
            {
                validationErrors.Add("Erro: E-mail inválido. O e-mail deve ser válido e ter o formato correto.");
            }
        }
    }
}
