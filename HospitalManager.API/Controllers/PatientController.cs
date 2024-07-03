using HospitalManager.Application.UseCases.Patients.GetById;
using HospitalManager.Application.UseCases.Patients.GetDocByCpf;
using HospitalManager.Application.UseCases.Patients.Register;
using HospitalManager.Communication.Requests.Patient;
using HospitalManager.Communication.Responses;
using HospitalManager.Communication.Responses.Patient;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponsePatientJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromForm] RequestPatientJson request)
        {
            var useCase = new RegisterPatientUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(typeof(ResponsePatientJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var useCase = new GetPatientByIdUseCase();

            var response = useCase.Execute(id);

            return Ok(response);
        }

        [HttpGet]
        [Route("getdoc/{cpf}")]
        [ProducesResponseType(typeof(ResponsePatientDocJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetPatientDocByCpf([FromRoute]string cpf)
        {
            var useCase = new GetPatientDocByCpfUseCase();

            var request = new RequestPatientDocJson { CPF = cpf };

            var response = useCase.Execute(request);

            if(response == null || response.imgDoc == null)
            {
                throw new ErrorOnValidationException("This patient don't have doc uploaded.");
            }

            return File(response.imgDoc, "image/png");
        }
    }
}
