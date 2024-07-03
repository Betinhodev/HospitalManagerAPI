using HospitalManager.Application.UseCases.Doctors.GetById;
using HospitalManager.Application.UseCases.Doctors.GetDocByCpf;
using HospitalManager.Application.UseCases.Doctors.Register;
using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    [Authorize(Roles = "doctor, admin")]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterDoctorJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromForm] RequestDoctorJson request)
    {
        var useCase = new RegisterDoctorUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [Authorize(Roles = "doctor, admin")]
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseRegisterDoctorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute]Guid id)
    {

        var useCase = new GetDoctorByIdUseCase();

        var response = useCase.Execute(id);

        return Ok(response);
    }

    [HttpGet]
    [Route("getdoc/{cpf}")]
    [ProducesResponseType(typeof(ResponseDoctorDocJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
    public IActionResult GetDocById([FromRoute]string cpf)
    {
        var useCase = new GetDoctorDocByCpfUseCase();

        var request = new RequestDoctorDocJson { CPF = cpf };

        var response = useCase.Execute(request);

        if (response == null || response.imgDoc == null)
        {
            throw new ErrorOnValidationException("This doctor don't have doc uploaded.");
        }

        return File(response.imgDoc, "image/png");
    }
}

