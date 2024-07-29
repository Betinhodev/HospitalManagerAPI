using HospitalManager.Application.UseCases.Appointments.GetById;
using HospitalManager.Application.UseCases.Appointments.Register;
using HospitalManager.Application.UseCases.Appointments.Update;
using HospitalManager.Communication.Requests.Appointment;
using HospitalManager.Communication.Requests.Appointments;
using HospitalManager.Communication.Responses;
using HospitalManager.Communication.Responses.Appointment;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HospitalManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly RegisterAppointmentUseCase _registerAppointmentUseCase;
        private readonly GetAppointmentByIdUseCase _getAppointmentByIdUseCase;
        private readonly UpdateAppointmentUseCase _updateAppointmentUseCase;

        public AppointmentController(RegisterAppointmentUseCase registerAppointmentUseCase, GetAppointmentByIdUseCase getAppointmentByIdUseCase, UpdateAppointmentUseCase updateAppointmentUseCase)
        {
            _registerAppointmentUseCase = registerAppointmentUseCase;
            _getAppointmentByIdUseCase = getAppointmentByIdUseCase;
            _updateAppointmentUseCase = updateAppointmentUseCase;

        }

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
            var response = _registerAppointmentUseCase.Execute(request);

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

            var response = _getAppointmentByIdUseCase.Execute(id);

            var userCpf = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var dbContext = new HospitalManagerDbContext();

            var appointment = dbContext.Appointments.Include(appointment => appointment.Patient).FirstOrDefault(appointment => appointment.AppointmentId == id);

            if (userCpf == null)
                throw new ErrorOnValidationException("User is not authenticated");
            if(appointment == null)
                throw new NotFoundException("Appointment not found.");
            if (userRole == "patient" && appointment.Patient.CPF != userCpf)
                throw new ErrorOnValidationException("You are not authorized to access this appointment.");
            return Ok(response);
        }

        [Authorize(Roles = "doctor, admin")]
        [HttpPut]
        [ProducesResponseType(typeof(ResponseAppointmentJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult Update([FromForm] RequestUpdateAppointmentJson request)
        {
            var useCase = _updateAppointmentUseCase;

            var response = useCase.Execute(request);

            return Ok(response);
        }
    }
}
