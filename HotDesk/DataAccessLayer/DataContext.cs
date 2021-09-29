using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DataContext : DbContext
    {
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed default roles
            string adminRoleName = "admin";
            string userRoleName = "user";
            Role adminRole = new() { Id = 1, RoleName = adminRoleName };
            Role userRole = new() { Id = 2, RoleName = userRoleName };

            // Seed default admin
            string adminLogin = "admin";
            string adminPassword = "admin";
            User adminUser = new() { Id = 1, Login = adminLogin, Password = adminPassword, RoleId = adminRole.Id };

            // Seed default devices
            var defaultDevices = new Device[]
            {
                new Device(){ Id = 1, DeviceName = "Laptop"},
                new Device(){ Id = 2, DeviceName = "Mouse"},
                new Device(){ Id = 3, DeviceName = "Keyboard"},
                new Device(){ Id = 4, DeviceName = "Lamp"},
                new Device(){ Id = 5, DeviceName = "Monitor"},
                new Device(){ Id = 6, DeviceName = "Second Monitor"}
            };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            modelBuilder.Entity<Device>().HasData(defaultDevices);
            base.OnModelCreating(modelBuilder);
        }
    }
}
