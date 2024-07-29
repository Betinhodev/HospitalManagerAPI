using HospitalManager.Communication.Requests.Covenant;
using HospitalManager.Communication.Responses.Covenant;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories;
using HospitalManager.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Application.UseCases.Covenants.RegisterCovenantUseCase
{
    public class RegisterCovenantUseCase
    {
        private readonly ICovenantRepository _covenantRepository;
        public RegisterCovenantUseCase(ICovenantRepository covenantRepository)
        {
            _covenantRepository = covenantRepository;
        }
        public ResponseCovenantJson Execute(RequestCovenantJson request)
        {

            var entity = new Covenant
            {
                CovenantName = request.CovenantName
            };

            _covenantRepository.Add(entity);

            return new ResponseCovenantJson
            {
                CovenantId = entity.CovenantId,
                CovenantName = entity.CovenantName
            };
        }
    }
}
