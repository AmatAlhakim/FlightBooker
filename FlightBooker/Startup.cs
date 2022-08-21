using Microsoft.Owin;
using Owin;
using System;
using FlightBooker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FlightBooker.DAL;

[assembly: OwinStartupAttribute(typeof(FlightBooker.Startup))]
namespace FlightBooker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

		private void CreateRolesAndUsers()
		{
            //Decalre role manager object
            
            ApplicationDbContext context = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //To control normal user object
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
           
			//Create roles and defualt users 
			if (!roleManager.RoleExists("Admin"))
			{
                //Add new roll called admin in the database
                IdentityRole adminRole = new IdentityRole();
                adminRole.Name = "Admin";
                //create this roll in the database
                roleManager.Create(adminRole);
                //create a user to be the admin
                ApplicationUser adminUser = new ApplicationUser();
                adminUser.UserName = "amatalhakim@admin.com";
                adminUser.Email = "amatalhakim@admin.com";
                string pwd = "P@ssw0rd";
                IdentityResult result = userManager.Create(adminUser, pwd);

				if (result.Succeeded)//if the role and user was created successfully
				{
                    //Make this user the application admin
                    userManager.AddToRole(adminUser.Id, "Admin");
				}

            }
		}
	}
}
