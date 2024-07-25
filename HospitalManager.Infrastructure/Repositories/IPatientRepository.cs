namespace HospitalManager.Infrastructure.Repositories
{
    public interface IPatientRepository
    {
        bool hasCovenant(Guid patientId);
        string GetPatientName(Guid patientId);
    }
}
