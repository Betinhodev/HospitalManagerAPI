using HospitalManager.Communication.Responses.Appointment;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure.Repositories;

namespace HospitalManager.Application.UseCases.Appointments.GetById
{
    public class GetAppointmentByIdUseCase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public GetAppointmentByIdUseCase(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public ResponseAppointmentJson Execute(Guid id)
        {

            var entity = _appointmentRepository.GetById(id);

            if (entity is null)
            {
                throw new NotFoundException("A appointment with this id dont exist.");
            }

            string doctorName = _doctorRepository.GetDoctorName(id);
            string patientName = _patientRepository.GetPatientName(id);

            return new ResponseAppointmentJson
            {
                DoctorName = doctorName,
                PatientName = patientName,
                RegisterDate = entity.ScheduledDate,
                Status = entity.Status,
                Value = entity.Value
            };
        }
    }
}
