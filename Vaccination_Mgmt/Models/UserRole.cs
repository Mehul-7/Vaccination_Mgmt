namespace Vaccination_Mgmt.Models
{
    public class UserRole
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
