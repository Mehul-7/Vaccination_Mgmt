using Microsoft.EntityFrameworkCore;
using Vaccination_Mgmt.Models;

namespace Vaccination_Mgmt.Data
{
    public class VacDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Dose> Doses { get; set; }
        public DbSet<DoseMapping> DoseMappings { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConn"));
        }
        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<UserRole>().HasKey(sc => new { sc.UserID, sc.RoleId });
            builder.Entity<DoseMapping>().HasKey(sc => new { sc.UserId });
        } 

        
    }
}
