using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Communication.Enums
{
    public enum ExamType
    {
        [Description("Blood test")]
        BloodTest = 1,
        [Description("Analysis of body fluids.")]
        BodyFluids = 2,
        [Description("Audiometry")]
        Audiometry = 3,
        [Description("Urinalysis")]
        Urinalysis = 4,
        [Description("Biopsy")]
        Biopsy = 5
    }
}
