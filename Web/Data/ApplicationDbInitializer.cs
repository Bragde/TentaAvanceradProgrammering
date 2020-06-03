using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedUser(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@gamenet.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@gamenet.com",
                    EmailConfirmed = true,
                };

                var result = userManager.CreateAsync(user, "Admin123!").Result;
            }
            if (userManager.FindByEmailAsync("daniel@gamenet.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "Daniel",
                    Email = "daniel@gamenet.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Daniel123!").Result;
            }
        }
    }
}
