using HospitalManager.Application.UseCases.Doctors.GetById;
using HospitalManager.Application.UseCases.Doctors.GetDocByCpf;
using HospitalManager.Application.UseCases.Doctors.GetRelatedAppointments;
using HospitalManager.Application.UseCases.Doctors.Register;
using HospitalManager.Communication.Enums;
using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure.Repositories.Interfaces;
using HospitalManager.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace HospitalManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly ILogger<DoctorController> _logger;
    private readonly GetRelatedAppointmentsUseCase _getRelatedAppointmentsUseCase;
    private readonly GetDoctorByIdUseCase _getDoctorByIdUseCase;
    private readonly GetDoctorDocByCpfUseCase _getDoctorDocByCpfUseCase;

    public DoctorController(ILogger<DoctorController> logger, GetRelatedAppointmentsUseCase getRelatedAppointmentsUseCase, GetDoctorByIdUseCase getDoctorByIdUseCase, GetDoctorDocByCpfUseCase getDoctorDocByCpfUseCase)
    {
        _logger = logger;
        _getRelatedAppointmentsUseCase = getRelatedAppointmentsUseCase;
        _getDoctorByIdUseCase = getDoctorByIdUseCase;
        _getDoctorDocByCpfUseCase = getDoctorDocByCpfUseCase;
    }

    [Authorize(Roles = "doctor, admin")]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterDoctorJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromForm] RequestDoctorJson request)
    {
        var useCase = new RegisterDoctorUseCase();

        var response = useCase.Execute(request);

        _logger.LogInformation($"Doctor {request.Name} has been included.");
        return Created(string.Empty, response);
    }

    [Authorize(Roles = "doctor, admin")]
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseRegisterDoctorJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute]Guid id)
    {

        var useCase = _getDoctorByIdUseCase;

        var response = useCase.Execute(id);
        _logger.LogInformation($"Get request for {id} doctor.");
        return Ok(response);
    }

    [HttpGet]
    [Route("getdoc/{cpf}")]
    [ProducesResponseType(typeof(ResponseDoctorDocJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson),StatusCodes.Status404NotFound)]
    public IActionResult GetDocById([FromRoute]string cpf)
    {
        var useCase = _getDoctorDocByCpfUseCase;

        var request = new RequestDoctorDocJson { CPF = cpf };

        var response = useCase.Execute(request);

        if (response == null || response.imgDoc == null)
        {
            throw new ErrorOnValidationException("This doctor don't have doc uploaded.");
        }

        return File(response.imgDoc, "image/png");
    }

    [HttpGet]
    [Route("appointments")]

    public IActionResult GetRelatedAppointments([FromQuery] string cpf, AppointmentStatus status)
    {
        var useCase = _getRelatedAppointmentsUseCase;

        var request = new RequestDoctorRelatedAppointmentsJson
        {
            Status = status,
            CPF = cpf
        };

        var response = useCase.Execute(request);

        return Ok(response);
    }
}

