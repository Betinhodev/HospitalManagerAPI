using HospitalManager.Application.UseCases.Doctors.GetById;
using HospitalManager.Application.UseCases.Doctors.Register;
using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses;
using HospitalManager.Communication.Responses.Doctor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterDoctorJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestDoctorJson request)
    {
        var useCase = new RegisterDoctorUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }

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

}

