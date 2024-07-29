using HospitalManager.Communication.Requests.Patient;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories.Interfaces;

namespace HospitalManager.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalManagerDbContext _context;
        public PatientRepository(HospitalManagerDbContext context) 
        {
            _context = context;
        }

        public void Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
        }

        public Patient GetByCpf(string patientCpf)
        {
            return _context.Patients.FirstOrDefault(p => p.CPF == patientCpf);
        }

        public Patient GetById(Guid patientId)
        {
            return _context.Patients.FirstOrDefault(p => p.PatientId == patientId);
        }

        public string GetPatientName(Guid patientId)
        {
            return _context.Patients.Where(p => p.PatientId == patientId).Select(p => p.Name).FirstOrDefault();
        }

        public bool hasCovenant(Guid patientId)
        {
            return _context.Patients.Any(p => p.PatientId == patientId && p.HasCovenant);
        }

        public void Validate(RequestPatientJson request)
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
                if (request.imgDoc == null || request.imgDoc.Length == 0)
                {
                    throw new ErrorOnValidationException("You need to upload a doc image.");
                }
        }
    }
}
