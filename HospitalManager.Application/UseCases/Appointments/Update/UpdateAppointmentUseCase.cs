using HospitalManager.Communication.Enums;
using HospitalManager.Communication.Requests.Appointments;
using HospitalManager.Communication.Responses.Appointments;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories.Interfaces;
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
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentReturnRepository _appointmentReturnRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IReportRepository _reportRepository;
        public UpdateAppointmentUseCase(IReportRepository reportRepository, IAppointmentRepository appointmentRepository, IAppointmentReturnRepository appointmentReturnRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _reportRepository = reportRepository;
            _appointmentRepository = appointmentRepository;
            _appointmentReturnRepository = appointmentReturnRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }
        public ResponseUpdateAppointmentJson Execute(RequestUpdateAppointmentJson request)
        { 

            var entity = _appointmentRepository.GetById(request.AppointmentId);

            if((DateTime.Now - entity.ScheduledDate).Days > 30)
            {
                throw new ErrorOnValidationException("You can't open an Appointment Return after 30 days.");
            }

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
                Status = AppointmentStatus.Scheduled
            };

            if (request.NeedReturn == true)
            {
                _appointmentReturnRepository.Add(appointmentReturn);
            }

            var doctorName = _doctorRepository.GetDoctorName(entity.DoctorId);
            var patientName = _patientRepository.GetPatientName(entity.PatientId);

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
                _reportRepository.Add(reportCreate);
            }



            return new ResponseUpdateAppointmentJson
            {
                AppointmentReturnId = appointmentReturn.AppointmentId
            };
        }

    }
}
