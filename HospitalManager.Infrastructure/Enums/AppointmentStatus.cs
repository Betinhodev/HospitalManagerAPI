﻿using System.ComponentModel;

namespace HospitalManager.Infrastructure.Enums
{
    public enum AppointmentStatus
    {
        [Description("Scheduled Appointment")]
        Scheduled = 1,
        [Description("Completed Appointment")]
        Completed = 2,
        [Description("Expired Appointment")]
        Expired = 3,

    }
}
