using HospitalManager.Communication.Requests.Patient;
using HospitalManager.Infrastructure.Entities;

namespace HospitalManager.Infrastructure.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        void Validate(RequestPatientJson request);
        bool hasCovenant(Guid patientId);
        string GetPatientName(Guid patientId);
        Patient GetById(Guid patientId);
        Patient GetByCpf(string patientCpf);
        void Add(Patient patient);
    }
}
