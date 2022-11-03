namespace Vaccination_Mgmt.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public IList<UserRole>? UserRole { get; set; }
    }
}
