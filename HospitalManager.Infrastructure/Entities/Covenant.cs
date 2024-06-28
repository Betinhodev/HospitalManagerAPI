using System.ComponentModel.DataAnnotations;

namespace HospitalManager.Infrastructure.Entities
{
    public class Covenant
    {
        [Key]
        public Guid CovenantId { get; set; } = new Guid();
        public string CovenantName { get; set; } = string.Empty;
    }
}
