using HospitalManager.Application.UseCases.Doctors.Register;
using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HospitalManager.Application.UseCases.Covenants.RegisterCovenantUseCase;
using HospitalManager.Communication.Requests.Covenant;
using HospitalManager.Communication.Responses.Covenant;
using HospitalManager.Application.UseCases.Doctors.GetById;
using HospitalManager.Application.UseCases.Covenants.GetById;

namespace HospitalManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovenantController : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseCovenantJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestCovenantJson request)
        {
            var useCase = new RegisterCovenantUseCase();

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
            var useCase = new GetCovenantByIdUseCase();

            var response = useCase.Execute(id);

            return Ok(response);
        }

    }
}
