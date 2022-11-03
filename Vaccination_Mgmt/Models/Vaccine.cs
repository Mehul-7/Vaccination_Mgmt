namespace Vaccination_Mgmt.Models
{
    public class Vaccine
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DoseMapping? DoseMapping { get; set; }
    }
}
