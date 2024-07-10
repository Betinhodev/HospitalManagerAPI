using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Appointment;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Application.UseCases.Doctors.GetRelatedAppointments
{
    public class GetRelatedAppointmentsUseCase
    {  
        public List<Appointment> Execute(RequestDoctorRelatedAppointmentsJson request)
        {
            var dbContext = new HospitalManagerDbContext();

            var doctorId = dbContext.Doctors.Where(d => d.CPF == request.CPF).Select(d => d.DoctorId).FirstOrDefault();
 
            var entity = dbContext.Appointments.Where(appointment => appointment.DoctorId == doctorId).Where(appointment => appointment.Status == request.Status).ToList();
        
            if(entity == null)
            {
                throw new ErrorOnValidationException("CPF not found.");
            }

            //var list = new ResponseAppointmentJson
            //{
            //    DoctorName = entity.Doctor.Name,
            //    PatientName = entity.Patient.Name,
            //    RegisterDate = entity.ScheduledDate,
            //    Status = entity.Status,
            //    Value = entity.Value,
            //};

            return entity;
        }
    }
}
