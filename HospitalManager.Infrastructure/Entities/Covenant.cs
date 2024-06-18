using System.ComponentModel.DataAnnotations;

namespace HospitalManager.Infrastructure.Entities
{
    public class Covenant
    {
        [Key]
        public int Id { get; set; }
        public string CovenantName { get; set; } = string.Empty;
    }
}
