using HospitalManager.Communication.Enums;
using HospitalManager.Communication.Requests.Appointment;
using HospitalManager.Communication.Responses.Appointment;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories;

namespace HospitalManager.Application.UseCases.Appointments.Register
{
    public class RegisterAppointmentUseCase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public RegisterAppointmentUseCase(IPatientRepository patientRepository, IAppointmentRepository appointmentRepository, IDoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;

        }

        public ResponseAppointmentJson Execute(RequestAppointmentJson request)
        {
            Validate(request);

            var hasCovenant = _patientRepository.hasCovenant(request.PatientId);
            decimal value = hasCovenant ? 0 : 100;


            var entity = new Appointment
            {
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                ScheduledDate = request.ScheduledDate,
                Status = AppointmentStatus.Scheduled,
                Value = value
            };

            _appointmentRepository.Add(entity);


            string doctorName = _doctorRepository.GetDoctorName(request.PatientId);
            string patientName = _patientRepository.GetPatientName(request.PatientId);

            return new ResponseAppointmentJson
            {
                DoctorName = doctorName,
                PatientName = patientName,
                RegisterDate = entity.ScheduledDate,
                Status = entity.Status,
                Value = value
            };
        }

        private void Validate(RequestAppointmentJson request)
        {
        }
    }
}
