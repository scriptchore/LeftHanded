using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace INFRASTRUCTURE.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    Displayname = "Vicki",
                    Email = "vickiviki@gmail.com",
                    UserName ="vickiviki@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Vicki",
                        LastName = "Viki",
                        Street = "100001",
                        City = "Zagreb",
                        State = "Crotia",
                        ZipCode = "100000"

                    }
                };

                await userManager.CreateAsync(user, "Feragamo@13");
            }

        }
    }
}