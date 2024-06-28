using HospitalManager.Communication.Responses.Appointment;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Enums;

namespace HospitalManager.Application.UseCases.Appointments.GetById
{
    public class GetAppointmentByIdUseCase
    {
        public ResponseAppointmentJson Execute(Guid id)
        {
            var dbContext = new HospitalManagerDbContext();

            var entity = dbContext.Appointments.FirstOrDefault(appointment => appointment.AppointmentId == id);

            if (entity is null)
            {
                throw new NotFoundException("A appointment with this id dont exist.");
            }

            string doctorName = dbContext.Doctors.Where(d => d.DoctorId == entity.DoctorId).Select(d => d.Name).FirstOrDefault();
            string patientName = dbContext.Patients.Where(p => p.PatientId == entity.PatientId).Select(p => p.Name).FirstOrDefault();

            return new ResponseAppointmentJson
            {
                DoctorName = doctorName,
                PatientName = patientName,
                RegisterDate = entity.RegisterDate,
                Status = entity.Status,
                Value = entity.Value
            };
        }
    }
}
