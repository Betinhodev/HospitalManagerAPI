using HospitalManager.Application.UseCases.Covenants.GetById;
using HospitalManager.Application.UseCases.Covenants.RegisterCovenantUseCase;
using HospitalManager.Communication.Requests.Covenant;
using HospitalManager.Communication.Responses;
using HospitalManager.Communication.Responses.Covenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovenantController : ControllerBase
    {
        private readonly RegisterCovenantUseCase _registerCovenantUseCase;
        private readonly GetCovenantByIdUseCase _getCovenantByIdUseCase;
        public CovenantController(RegisterCovenantUseCase registerCovenantUseCase, GetCovenantByIdUseCase getCovenantByIdUseCase)
        {
            _registerCovenantUseCase = registerCovenantUseCase;
            _getCovenantByIdUseCase = getCovenantByIdUseCase;

        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCovenantJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestCovenantJson request)
        {
            var useCase = _registerCovenantUseCase;

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [Authorize(Roles = "doctor, admin")]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseCovenantJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var useCase = _getCovenantByIdUseCase;

            var response = useCase.Execute(id);

            return Ok(response);
        }

    }
}
