﻿using HospitalManager.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Communication.Requests.Appointments
{
    public class RequestUpdateAppointmentJson
    {
        public Guid AppointmentId { get; set; }
        public AppointmentStatus Status { get; set; }
        public bool NeedReturn { get; set; }
        public DateTime? ScheduledDate { get; set; }
    }
}
