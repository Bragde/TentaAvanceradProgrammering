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
                    Id = "d28888e9-2ba9-473a-a40f-e38cb54f9b35",
                    UserName = "Admin",
                    Email = "admin@gamenet.com",
                    EmailConfirmed = true,
                };

                var result = userManager.CreateAsync(user, "Admin123!").Result;
            }
        }
    }
}
