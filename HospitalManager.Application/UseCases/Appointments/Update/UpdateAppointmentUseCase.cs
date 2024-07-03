using HospitalManager.Communication.Requests.Appointments;
using HospitalManager.Communication.Responses.Appointments;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Application.UseCases.Appointments.Update
{
    public class UpdateAppointmentUseCase
    {
        public ResponseUpdateAppointmentJson Execute(RequestUpdateAppointmentJson request)
        {
            var dbContext = new HospitalManagerDbContext();

            var entity = dbContext.Appointments.FirstOrDefault(appointment => appointment.AppointmentId == request.AppointmentId);

            entity.Status = request.Status;

            var appointmentReturn = new AppointmentReturn
            {
                ReturnId = new Guid(),
                DoctorId = entity.DoctorId,
                PatientId = entity.PatientId,
                AppointmentId = entity.AppointmentId,
                ScheduledDate = request.ScheduledDate,
                Status = Infrastructure.Enums.AppointmentStatus.Scheduled
            };

            if (request.NeedReturn == true)
            {
                dbContext.AppointmentReturns.Add(appointmentReturn);
            }

             dbContext.SaveChanges();

            return new ResponseUpdateAppointmentJson
            {
                AppointmentReturnId = appointmentReturn.AppointmentId
            };
        }
    }
}
