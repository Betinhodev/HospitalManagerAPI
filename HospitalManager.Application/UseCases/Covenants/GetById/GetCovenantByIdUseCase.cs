using HospitalManager.Communication.Requests.Covenant;
using HospitalManager.Communication.Responses.Covenant;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Application.UseCases.Covenants.GetById
{
    public class GetCovenantByIdUseCase
    {
        public ResponseCovenantJson Execute(Guid id)
        {
            var dbContext = new HospitalManagerDbContext();

            var entity = dbContext.Covenants.FirstOrDefault(covenant => covenant.CovenantId == id);

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
