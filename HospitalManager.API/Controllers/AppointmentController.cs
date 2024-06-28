using HospitalManager.Application.UseCases.Appointments.GetById;
using HospitalManager.Application.UseCases.Appointments.Register;
using HospitalManager.Communication.Requests.Appointment;
using HospitalManager.Communication.Responses;
using HospitalManager.Communication.Responses.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        /// <summary>
        /// register a new appointment with date, patient and a doctor.
        /// </summary>
        /// <param name="RequestAppointmentJson">Object with fields for register an appointment.</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">If register suceed.</response>
        /// <response code="400">If register don't suceed.</response>
        [Authorize(Roles = "doctor, admin")]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseAppointmentJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestAppointmentJson request)
        {
            var useCase = new RegisterAppointmentUseCase();

            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }

        /// <summary>
        /// get info about an appointment by id.
        /// </summary>
        /// <param name="RequestAppointmentJson">Object with fields for get an appointment by id.</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">If appointment exists.</response>
        /// <response code="404">If appointment don't exists.</response>
        [Authorize(Roles = "patient, doctor, admin")]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseAppointmentJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var useCase = new GetAppointmentByIdUseCase();

            var response = useCase.Execute(id);

            return Ok(response);
        }
    }
}
