using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any())
                return;

            var user = new AppUser { UserName = "admin", Email = "admin@email.com", IsAdmin = true };

            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}