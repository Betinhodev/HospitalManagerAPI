using HospitalManager.Communication.Responses.Patient;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;

namespace HospitalManager.Application.UseCases.Patients.GetById
{
    public class GetPatientByIdUseCase
    {
        public ResponsePatientJson Execute(Guid id)
        {
            var dbContext = new HospitalManagerDbContext();

            var entity = dbContext.Patients.FirstOrDefault(patient => patient.Id == id);

            if (entity is null)
            {
                throw new NotFoundException("A patient with this id dont exist.");
            }

            return new ResponsePatientJson
            {
                Id = entity.Id,
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
