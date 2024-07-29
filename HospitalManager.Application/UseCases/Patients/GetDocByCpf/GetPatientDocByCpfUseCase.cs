using HospitalManager.Communication.Requests.Patient;
using HospitalManager.Communication.Responses.Patient;
using HospitalManager.Exceptions;
using HospitalManager.Infrastructure;
using HospitalManager.Infrastructure.Entities;
using HospitalManager.Infrastructure.Repositories.Interfaces;
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
        private readonly IPatientRepository _patientRepository;
        public GetPatientDocByCpfUseCase(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public ResponsePatientDocJson Execute(RequestPatientDocJson request)
        {
            var entity = _patientRepository.GetByCpf(request.CPF);

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
