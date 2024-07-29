using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Requests.Patient;
using HospitalManager.Communication.Responses.Patient;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories;
using HospitalManager.Infrastructure.Repositories.Interfaces;
using HospitalManager.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HospitalManager.Application.UseCases.Patients.Register
{
    public class RegisterPatientUseCase
    {
        private readonly ILogger<RegisterPatientUseCase> logger;
        private readonly IPatientRepository _patientRepository;
        PassHasherService<Patient> hashedPass = new PassHasherService<Patient>();

        public RegisterPatientUseCase(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public ResponsePatientJson Execute(RequestPatientJson request)
        {
            _patientRepository.Validate(request);

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

            _patientRepository.Add(entity);

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

        
    }
}
