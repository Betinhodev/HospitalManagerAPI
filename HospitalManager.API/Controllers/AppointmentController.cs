using HospitalManager.Application.UseCases.Appointments.Register;
using HospitalManager.Communication.Requests.Appointment;
using HospitalManager.Communication.Responses;
using HospitalManager.Communication.Responses.Appointment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseAppointmentJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestAppointmentJson request)
        {
            var useCase = new RegisterAppointmentUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
    }
}
