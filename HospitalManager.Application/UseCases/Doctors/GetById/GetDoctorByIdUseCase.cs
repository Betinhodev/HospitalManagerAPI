using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Repositories.Interfaces;
using HospitalManager.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HospitalManager.Application.UseCases.Doctors.GetById
{
    public class GetDoctorByIdUseCase
    {
        private readonly IDoctorRepository _doctorRepository;
        public GetDoctorByIdUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public ResponseRegisterDoctorJson Execute(Guid id)
        {

            var entity = _doctorRepository.GetById(id);
        
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
