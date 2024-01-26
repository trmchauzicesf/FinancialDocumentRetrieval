using FinancialDocumentRetrieval.BL.Interface;
using FinancialDocumentRetrieval.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace FinancialDocumentRetrieval.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            _authManager = authManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> RegisterAsync([FromBody] ApiUserDto apiUserDto)
        {
            _logger.LogInformation($"Registration Attempt for {apiUserDto.Email}");
            var errors = await _authManager.RegisterAsync(apiUserDto);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation($"LoginAsync Attempt for {loginDto.Email} ");
            var authResponse = await _authManager.LoginAsync(loginDto);

            if (authResponse.Token == null && authResponse.UserId == null)
            {
                return Unauthorized();
            }

            return Ok(authResponse);
        }
    }
}
