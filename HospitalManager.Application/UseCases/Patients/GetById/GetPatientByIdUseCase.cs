using HospitalManager.Communication.Responses.Patient;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Repositories.Interfaces;

namespace HospitalManager.Application.UseCases.Patients.GetById
{
    public class GetPatientByIdUseCase
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientByIdUseCase(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public ResponsePatientJson Execute(Guid id)
        { 
            var entity = _patientRepository.GetById(id);

            if (entity is null)
            {
                throw new NotFoundException("A patient with this id dont exist.");
            }

            return new ResponsePatientJson
            {
                Id = entity.PatientId,
                CPF = entity.CPF,
                Name = entity.Name,
                Address = entity.Address,
                BirthDate = entity.BirthDate,
                HasCovenant = entity.HasCovenant,
                CovenantId = entity.CovenantId
            };
        }
    }
}
