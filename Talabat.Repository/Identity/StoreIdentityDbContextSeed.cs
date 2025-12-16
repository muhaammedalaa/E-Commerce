using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class StoreIdentityDbContextSeed
    {
        public static async Task SeedAppUserAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Count() == 0)
            {

                var user = new AppUser()
                {
                    Email = "mohamedalaa272001@gmail.com",
                    DisplayName = "Mohamed Alaa",
                    UserName = "muhaammedalaa",
                    PhoneNumber = "01202335426",
                    Address = new Address()
                    {
                        FirstName = "Mohamed",
                        LastName = "Alaa",
                        City = "Cairo",
                        Country = "Egypt"
                    }
                };
               await userManager.CreateAsync(user, "P@ssw0rd");
            }

        }
    }
}
