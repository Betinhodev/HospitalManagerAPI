using HospitalManager.Communication.Requests.Covenant;
using HospitalManager.Communication.Responses.Covenant;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Application.UseCases.Covenants.GetById
{
    public class GetCovenantByIdUseCase
    {
        private readonly ICovenantRepository _covenantRepository;
        public GetCovenantByIdUseCase(ICovenantRepository covenantRepository)
        {
            _covenantRepository = covenantRepository;
        }
        public ResponseCovenantJson Execute(Guid id)
        {

            var entity = _covenantRepository.GetById(id);

            if(entity is null)
            {
                throw new NotFoundException("A covenant with this id don't exist.");
            }

            return new ResponseCovenantJson
            {
                CovenantId = entity.CovenantId,
                CovenantName = entity.CovenantName

            };
        }
    }
}
