using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure;
using HospitalManager.Communication.Responses.Appointment;
using HospitalManager.Communication.Requests.Appointment;
using HospitalManager.Infrastructure.Enums;

namespace HospitalManager.Application.UseCases.Appointments.Register
{
    public class RegisterAppointmentUseCase
    {
        public ResponseAppointmentJson Execute(RequestAppointmentJson request)
        {
            Validate(request);

            var dbContext = new HospitalManagerDbContext();

            var hasCovenant = dbContext.Patients.Where(p => p.PatientId == request.PatientId).Any(p => p.HasCovenant == true);
            decimal value = hasCovenant ? 0 : 100;
            

            var entity = new Appointment
            {
                DoctorId = request.DoctorId,
                PatientId = request.PatientId,
                ScheduledDate = request.ScheduledDate,
                Status = AppointmentStatus.Scheduled,
                Value = value
            };


            dbContext.Appointments.Add(entity);
            dbContext.SaveChanges();

            string doctorName = dbContext.Doctors.Where(d => d.DoctorId == entity.DoctorId).Select(d => d.Name).FirstOrDefault();
            string patientName = dbContext.Patients.Where(p => p.PatientId == entity.PatientId).Select(p => p.Name).FirstOrDefault();

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
            var dbContext = new HospitalManagerDbContext();
        }
    }
}
