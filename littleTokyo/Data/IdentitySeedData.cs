using littleTokyo.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Identity;

namespace littleTokyo.Data
{
    public class IdentitySeedData
    {

        public static async Task Initialize(littleTokyoContext context,
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            string adminRole = "Admin";
            string memberRole = "Member";
            string password = "P@ssw0rd";

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            if (await roleManager.FindByNameAsync(memberRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(memberRole));
            }

            if (await userManager.FindByNameAsync("admin@chester.ac.uk") == null) {
                var user = new ApplicationUser
                {
                    FirstName = "Chester",
                    LastName = "Admin",
                    UserName = "admin@chester.ac.uk",
                    Email = "admin@chester.ac.uk",
                    PhoneNumber = "01244 123456"
                };

                

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }
            
            if (await userManager.FindByNameAsync("member@chester.ac.uk") == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Chester",
                    LastName = "Member",
                    UserName = "member@chester.ac.uk",
                    Email = "member@chester.ac.uk",
                    PhoneNumber = "01233 123456"
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, memberRole);
                }
            }
        }

       

    }
}
