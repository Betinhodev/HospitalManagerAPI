namespace HospitalManager.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalManagerDbContext _context;
        public PatientRepository(HospitalManagerDbContext context) 
        {
            _context = context;
        }

        public string GetPatientName(Guid patientId)
        {
            return _context.Patients.Where(p => p.PatientId == patientId).Select(p => p.Name).FirstOrDefault();
        }

        public bool hasCovenant(Guid patientId)
        {
            return _context.Patients.Any(p => p.PatientId == patientId && p.HasCovenant);
        }
    }
}
