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
                    UserName = "admin",
                    Email = "admin@gamenet.com",
                    EmailConfirmed = true,
                    ShoppingCartId = "90d6da79-e0e2-4ba8-bf61-2d94d90df810"
                };

                var result = userManager.CreateAsync(user, "Password1!").Result;
            }
        }
    }
}
