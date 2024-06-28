using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HospitalManager.Application.UseCases.Doctors.GetById
{
    public class GetDoctorByIdUseCase
    {
        public ResponseRegisterDoctorJson Execute(Guid id)
        {
            var dbContext = new HospitalManagerDbContext();

            var entity = dbContext.Doctors.FirstOrDefault(doctor => doctor.DoctorId == id);
        
            if(entity is null)
            {
                throw new NotFoundException("A doctor with this id dont exist.");
            }


            return new ResponseRegisterDoctorJson
            {
                Id = entity.DoctorId,
                Name = entity.Name
            };
        }
    }
}
