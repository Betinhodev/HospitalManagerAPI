using HospitalManager.Communication.Requests.Authentication;
using HospitalManager.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace HospitalManager.Infrastructure.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;
        private readonly HospitalManagerDbContext _context;
        private readonly ILogger<AuthService> _logger;
        PassHasher<Patient> hashedPatientPass = new PassHasher<Patient>();
        PassHasher<Doctor> hashedDoctorPass = new PassHasher<Doctor>();

        public AuthService(IConfiguration configuration, HospitalManagerDbContext context, ILogger<AuthService> logger)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public object AuthUser(RequestAuthJson requestAuth)
        {
            var patient = _context.Patients.FirstOrDefault(patient => patient.CPF == requestAuth.CPF);
            
            if(patient != null)
            {
                var isValidPass = hashedPatientPass.VerifyHashedPassword(patient, patient.Password, requestAuth.Password);
                if(isValidPass == PasswordVerificationResult.Success)
                {
                    _logger.LogInformation($"Patient {patient.Name} authenticated.");
                    return GenerateToken(patient.CPF, "patient");
                }
            }

            var doctor = _context.Doctors.FirstOrDefault(doctor => doctor.CPF == requestAuth.CPF);
            if (doctor != null)
            {
                var isValidPass = hashedDoctorPass.VerifyHashedPassword(doctor, doctor.Password, requestAuth.Password);
                if(isValidPass == PasswordVerificationResult.Success)
                {
                    _logger.LogInformation($"Doctor {doctor.Name} authenticated.");
                    return GenerateToken(doctor.CPF, "doctor");
                }
            }

            if(requestAuth.CPF == "admin" && requestAuth.Password == "root")
            {
                _logger.LogInformation("Admin logged in.");
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
