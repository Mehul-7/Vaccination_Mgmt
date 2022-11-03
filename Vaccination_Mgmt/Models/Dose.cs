using System.ComponentModel.DataAnnotations.Schema;

namespace Vaccination_Mgmt.Models
{
    public class Dose
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
        public DoseMapping? DoseMapping { get; set; }
        [NotMapped]
        public int VaccineId { get; set; }

    }
}
