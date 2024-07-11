using HospitalManager.Communication.Enums;
using HospitalManager.Communication.Requests.Appointments;
using HospitalManager.Communication.Responses.Appointments;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
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

            if (entity == null)
            {
                throw new ErrorOnValidationException("Appointment ID don't exist.");
            }

            if(request.NeedReturn & request.ScheduledDate == null)
            {
                throw new ErrorOnValidationException("You need to set a date to the appointment return.");
            }
            entity.Status = request.Status;
            

            var appointmentReturn = new AppointmentReturn
            {
                ReturnId = new Guid(),
                DoctorId = entity.DoctorId,
                PatientId = entity.PatientId,
                AppointmentId = entity.AppointmentId,
                ScheduledDate = request.ScheduledDate,
                Status = Communication.Enums.AppointmentStatus.Scheduled
            };

            if (request.NeedReturn == true)
            {
                dbContext.AppointmentReturns.Add(appointmentReturn);
            }

            var doctorName = dbContext.Doctors.Where(dn => dn.DoctorId == entity.DoctorId).Select(dn => dn.Name).FirstOrDefault();
            var patientName = dbContext.Patients.Where(pn => pn.PatientId == entity.PatientId).Select(pn => pn.Name).FirstOrDefault();

            if (request.Status == AppointmentStatus.Completed)
            {
                var reportCreate = new Report
                {
                    AppointmentId = entity.AppointmentId,
                    Description = request.Description,
                    ExamType = request.ExamType,
                    DoctorName = doctorName,
                    PatientName = patientName,
                    ReportId = new Guid()
                };

                dbContext.Reports.Add(reportCreate);
            }

             dbContext.SaveChanges();

            return new ResponseUpdateAppointmentJson
            {
                AppointmentReturnId = appointmentReturn.AppointmentId
            };
        }

    }
}
