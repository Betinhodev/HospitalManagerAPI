﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Infrastructure.Entities
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Password { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string BirthDate { get; set; } = string.Empty;
        public string DocImg { get; set; } = string.Empty;

        public bool HasCovenant { get; set; }

        public int CovenantId { get; set; }

        public int AppointmentId{ get; set; }
    }
}