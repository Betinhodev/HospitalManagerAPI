using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Appointment;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories.Interfaces;
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
        private readonly IDoctorRepository _doctorRepository;

        public GetRelatedAppointmentsUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public List<Appointment> Execute(RequestDoctorRelatedAppointmentsJson request)
        {
 
            var entity = _doctorRepository.GetRelatedAppointments(request);
        
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
