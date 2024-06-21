using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Services;

namespace HospitalManager.Application.UseCases.Doctors.Register;

public class RegisterDoctorUseCase
    {
    PassHasher<Doctor> hashedPass = new PassHasher<Doctor>();
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
            var doctorPassword = hashedPass.HashPassword(entity, request.Password);
            entity.Password = doctorPassword;

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


