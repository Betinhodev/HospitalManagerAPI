using HospitalManager.Communication.Requests.Patient;
using HospitalManager.Communication.Responses.Patient;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Application.UseCases.Patients.GetDocByCpf
{
    public class GetPatientDocByCpfUseCase
    {
        public ResponsePatientDocJson Execute(RequestPatientDocJson request)
        {
            var dbContext = new HospitalManagerDbContext();

            var entity = dbContext.Patients.FirstOrDefault(patient => patient.CPF == request.CPF);

            if(entity == null)
            {
                throw new NotFoundException("A patient with this cpf don't exist.");
            }

            var imgPath = entity.DocImg; 

            Byte[] b = System.IO.File.ReadAllBytes($"{imgPath}");

            return new ResponsePatientDocJson
            {
                imgDoc = b
            };
        }
    }
}
