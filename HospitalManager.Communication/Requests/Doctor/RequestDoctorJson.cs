﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Communication.Requests.Doctor
{
    public class RequestDoctorJson
    {
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public IFormFile imgDoc { get; set; }
    }
}
