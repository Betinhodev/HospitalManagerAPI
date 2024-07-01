using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManager.Communication.Responses.Covenant
{
    public class ResponseCovenantJson
    {
        public Guid CovenantId { get; set; }
        public string CovenantName { get; set; } = string.Empty;
    }
}
