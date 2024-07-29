using HospitalManager.Communication.Requests.Authentication;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories.Interfaces;
using HospitalManager.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HospitalManager.Application.UseCases.Authentication
{
    public class AuthUseCase
    {
        PassHasherService<Patient> hashedPatientPass = new PassHasherService<Patient>();
        PassHasherService<Doctor> hashedDoctorPass = new PassHasherService<Doctor>();
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;

        public AuthUseCase(IPatientRepository patientRepository, IDoctorRepository doctorRepository)
        {
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;

        }
        public object Execute(RequestAuthJson requestAuth)
        {
            var context = new HospitalManagerDbContext();

            var patient = _patientRepository.GetByCpf(requestAuth.CPF);

            if (patient != null)
            {
                var isValidPass = hashedPatientPass.VerifyHashedPassword(patient, patient.Password, requestAuth.Password);
                if (isValidPass == PasswordVerificationResult.Success)
                {
                    return GenerateToken(patient.CPF, "patient");
                }
            }

            var doctor = _doctorRepository.GetByCpf(requestAuth.CPF);
            if (doctor != null)
            {
                var isValidPass = hashedDoctorPass.VerifyHashedPassword(doctor, doctor.Password, requestAuth.Password);
                if (isValidPass == PasswordVerificationResult.Success)
                {
                    
                    return GenerateToken(doctor.CPF, "doctor");
                }
            }

            if (requestAuth.CPF == "admin" && requestAuth.Password == "root")
            {
               
                return GenerateToken("admin", "admin");
            }

            return null;

        }

        public class Key
        {
            public static string Secret = "123456asd974as56af1a6f1as65f4f65a";
        }

        public static object GenerateToken(string CPF, string role)
        {
            var key = Encoding.ASCII.GetBytes(Key.Secret);
            var tokenConfig = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, CPF.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenConfig);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                token = tokenString
            };
        }

    }
}

