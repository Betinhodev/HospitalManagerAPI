using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Requests.Patient;
using HospitalManager.Communication.Responses.Patient;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HospitalManager.Application.UseCases.Patients.Register
{
    public class RegisterPatientUseCase
    {
        private readonly ILogger<RegisterPatientUseCase> logger;
        PassHasherService<Patient> hashedPass = new PassHasherService<Patient>();
        public ResponsePatientJson Execute(RequestPatientJson request)
        {
            Validate(request);

            var dbContext = new HospitalManagerDbContext();

            Guid guidDocPatient = Guid.NewGuid();
            var imgPath = Path.Combine("C:\\Users\\gsnogueira\\source\\repos\\HospitalManager.API\\HospitalManager.API\\Images", $"{guidDocPatient}");

            using (FileStream stream = System.IO.File.Create(imgPath))
            {
                request.imgDoc.CopyToAsync(stream);
            }

            var entity = new Patient
            {
                Name = request.Name,
                CPF = request.CPF,
                Password = request.Password,
                Address = request.Address,
                BirthDate = request.BirthDate,
                HasCovenant = request.HasCovenant,
                CovenantId = request.CovenantId,
                DocImg = imgPath
            };

            var patientPassword = hashedPass.HashPassword(entity, request.Password);
            entity.Password = patientPassword;

            dbContext.Patients.Add(entity);
            dbContext.SaveChanges();

            return new ResponsePatientJson
            {
                Id = entity.PatientId,
                CPF = request.CPF,
                Name = request.Name,
                Address = request.Address,
                BirthDate = request.BirthDate,
                HasCovenant = request.HasCovenant,
                CovenantId = request.CovenantId
            };
        }

        private void Validate(RequestPatientJson request)
        {
            var dbContext = new HospitalManagerDbContext();

            if (dbContext.Patients.Any(patient => patient.CPF == request.CPF))
            {
                throw new ErrorOnValidationException("The CPF already exists in the database");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("Name field can't be null.");
            }
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ErrorOnValidationException("Password field can't be null.");
            }
            if(request.imgDoc == null || request.imgDoc.Length == 0)
            {
                throw new ErrorOnValidationException("You need to upload a doc image.");
            }
        }
    }
}
