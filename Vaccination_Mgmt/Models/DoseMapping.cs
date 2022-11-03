using System.ComponentModel.DataAnnotations;

namespace Vaccination_Mgmt.Models
{
    public class DoseMapping
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int DoseId { get; set; }
        public Dose? Dose { get; set; }
        public int VaccineId { get; set; }
        public Vaccine? Vaccine { get; set; }
    }
}
