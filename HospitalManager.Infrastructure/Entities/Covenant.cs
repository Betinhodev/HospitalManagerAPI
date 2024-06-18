using System.ComponentModel.DataAnnotations;

namespace HospitalManager.Infrastructure.Entities
{
    public class Covenant
    {
        [Key]
        public int CovenantId { get; set; }
        public string CovenantName { get; set; } = string.Empty;
    }
}
