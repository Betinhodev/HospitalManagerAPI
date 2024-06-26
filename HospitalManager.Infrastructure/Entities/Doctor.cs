﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Infrastructure.Entities
{
    public class Doctor
    {
        [Key]
        public Guid DoctorId { get; set; } = Guid.NewGuid();

        public string CPF { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string DocImg { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; }

        public ICollection<AppointmentReturn> Returns { get; set; }
    }
}
