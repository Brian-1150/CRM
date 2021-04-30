using CRM.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRM.WebMVC.Startup))]
namespace CRM.WebMVC
{
    public partial class Startup
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateSeedRoles();
        }
        private void CreateSeedRoles()
        {
            CreateAdmin();
            CreateEmployee();
        }
        private void CreateAdmin()
        {
            
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

            IdentityRole role = new IdentityRole();

            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);

                ApplicationUser user = new ApplicationUser
                {
                    UserName = "BusinessOwner",
                    Email = "owner@myBusiness.com",
                    EmailConfirmed = true
                };

                if (userManager.Create(user, "Test1!").Succeeded)
                    userManager.AddToRole(user.Id, "Admin");

            }
        }
        private void CreateEmployee()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

            IdentityRole role = new IdentityRole();

            if (!roleManager.RoleExists("Employee"))
            {
                role.Name = "Employee";
                roleManager.Create(role);

                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Employee1",
                    Email = "employee@yourBusiness.com",
                    EmailConfirmed = true
                };

                if (userManager.Create(user, "Test1!").Succeeded)
                    userManager.AddToRole(user.Id, "Employee");

            }

        }
    }
}
