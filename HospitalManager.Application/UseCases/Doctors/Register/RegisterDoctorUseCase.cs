using HospitalManager.Communication.Requests;
using HospitalManager.Communication.Responses;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace HospitalManager.Application.UseCases.Doctors.Register;

    public class RegisterDoctorUseCase
    {
        public ResponseRegisterDoctorJson Execute(RequestDoctorJson request)
        {
            Validate(request);

            var dbContext = new HospitalManagerDbContext();

            var entity = new Doctor
            {
                Name = request.Name,
                CPF = request.CPF,
                Password = request.Password
            };

            dbContext.Doctors.Add(entity);
            dbContext.SaveChanges();

            return new ResponseRegisterDoctorJson
            {
                Id = entity.Id
            };
        }

    private void Validate(RequestDoctorJson request)
    {
        var dbContext = new HospitalManagerDbContext();

        if (dbContext.Doctors.Any(doctor => doctor.CPF == request.CPF))
        {
            throw new ErrorOnValidationException("The CPF already exists in the database");
        } 
        if(string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ErrorOnValidationException("Name field can't be null.");
        }
        if (string.IsNullOrWhiteSpace(request.CPF))
        {
            throw new ErrorOnValidationException("CPF field can't be null.");
        }
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ErrorOnValidationException("Password field can't be null.");
        }
    }
}


