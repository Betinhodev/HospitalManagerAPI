using HospitalManager.Communication.Requests.Doctor;
using HospitalManager.Communication.Responses.Doctor;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories.Interfaces;
using HospitalManager.Infrastructure.Services;

namespace HospitalManager.Application.UseCases.Doctors.Register
{

    public class RegisterDoctorUseCase
    {
        PassHasherService<Doctor> hashedPass = new PassHasherService<Doctor>();
        private readonly IDoctorRepository _doctorRepository;
        public RegisterDoctorUseCase(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public ResponseRegisterDoctorJson Execute(RequestDoctorJson request)
        {
            _doctorRepository.Validate(request);

            Guid guidDocPatient = Guid.NewGuid();
            var imgPath = Path.Combine("C:\\Users\\gsnogueira\\source\\repos\\HospitalManager.API\\HospitalManager.API\\Images", $"{guidDocPatient}");

            using (FileStream stream = System.IO.File.Create(imgPath))
            {
                request.imgDoc.CopyToAsync(stream);
            }

            var entity = new Doctor
            {
                Name = request.Name,
                CPF = request.CPF,
                Password = request.Password,
                DocImg = imgPath
            };
            var doctorPassword = hashedPass.HashPassword(entity, request.Password);
            entity.Password = doctorPassword;

            _doctorRepository.Add(entity);

            return new ResponseRegisterDoctorJson
            {
                Id = entity.DoctorId,
                Name = entity.Name
            };
        }

        
    }
}

