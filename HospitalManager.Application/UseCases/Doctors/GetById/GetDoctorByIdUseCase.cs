using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HospitalManager.Application.UseCases.Doctors.GetById
{
    public class GetDoctorByIdUseCase
    {
        public ResponseRegisterDoctorJson Execute(Guid id)
        {
            var dbContext = new HospitalManagerDbContext();

            var entity = dbContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);
        
            if(entity is null)
            {
                throw new NotFoundException("A doctor with this id dont exist.");
            }

            return new ResponseRegisterDoctorJson
            {
                Id = entity.Id,
                Name = entity.Name,
                AppointmentId = entity.AppointmentId
            };
        }
    }
}
