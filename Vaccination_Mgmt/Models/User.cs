namespace Vaccination_Mgmt.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Contact { get; set; }
        public DateTime Dob { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
        public IList<UserRole>? UserRole { get; set; }
        public DoseMapping? DoseMapping { get; set; }
    }
}
