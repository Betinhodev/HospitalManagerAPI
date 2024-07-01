using HospitalManager.Communication.Requests.Covenant;
using HospitalManager.Communication.Responses.Covenant;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Application.UseCases.Covenants.RegisterCovenantUseCase
{
    public class RegisterCovenantUseCase
    {
        public ResponseCovenantJson Execute(RequestCovenantJson request)
        {
            var dbContext = new HospitalManagerDbContext();

            var entity = new Covenant
            {
                CovenantName = request.CovenantName
            };

            dbContext.Covenants.Add(entity);
            dbContext.SaveChanges();

            return new ResponseCovenantJson
            {
                CovenantId = entity.CovenantId,
                CovenantName = entity.CovenantName
            };
        }
    }
}
