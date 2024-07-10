using HospitalManager.Application.UseCases.Authentication;
using HospitalManager.Communication.Requests.Authentication;
using HospitalManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Auth([FromForm] RequestAuthJson auth)
        {

            var useCase = new AuthUseCase();

            var response = useCase.Execute(auth);

            if (response == null)
            {
                _logger.LogWarning("user or password incorrect.");
                return BadRequest("user or password incorrect.");
            }
            _logger.LogInformation($"User - {auth.CPF} logged in.");
            return Ok(response);
        }
    }
}
