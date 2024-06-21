using HospitalManager.Communication.Requests.Authentication;
using HospitalManager.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authenticationService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authenticationService, ILogger<AuthController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Auth([FromForm] RequestAuthJson auth)
        {

            var token = _authenticationService.AuthUser(auth);

            if (token == null)
            {
                _logger.LogWarning("user or password incorrect.");
                return BadRequest("user or password incorrect.");
            }

            return Ok(new { token });
        }
    }
}
