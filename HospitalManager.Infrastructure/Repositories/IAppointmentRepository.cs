﻿using HospitalManager.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Infrastructure.Repositories
{
    public interface IAppointmentRepository
    {
        Appointment GetById(Guid appointmentId);
        void Add(Appointment appointment);
    }
}
