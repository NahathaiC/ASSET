using API.Entities;
using API.Entities.PRAggregate;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public static class DbInitializer
    {
        public static async void Initialize(StoreContext context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User
                {
                    Id = 63310051,
                    UserName = "liew",
                    Email = "liew@test.com",
                    Position = "Interns",
                    Department = "IT",
                    Section = "IT",
                    Phone = "0811111111",
                    Status = "Active"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Emp");

                var approver = new User
                {
                    Id = 63310052,
                    UserName = "approver",
                    Email = "app@test.com",
                    Position = "Approver",
                    Department = "Approver",
                    Section = "Approver",
                    Phone = "0811111112",
                    Status = "Active"
                };
                await userManager.CreateAsync(approver, "Pa$$w0rd");
                await userManager.AddToRolesAsync(approver, new[] { "Emp", "Approver" });


                var admin = new User
                {
                    Id = 63310053,
                    UserName = "admin",
                    Email = "admin@test.com",
                    Position = "Admin",
                    Department = "Admin",
                    Section = "Admin",
                    Phone = "0811111113",
                    Status = "Active"
                };
                await userManager.CreateAsync(admin, "Pa$$w0rd");
                await userManager.AddToRolesAsync(admin, new[] { "Emp", "Approver", "Admin" });

            }

            if (context.Owners.Any()) return;
            var owners = new List<Owner>
            {
                new Owner
                {
                    Id = 1,
                    OwnerDesc = "ทรัพย์สินของ บริษัท"
                },
                new Owner
                {
                    Id = 2,
                    OwnerDesc = "ทรัพย์สินของ คู่สัญญา"
                }
            };

            foreach (var owner in owners)
            {
                context.Owners.Add(owner);
            }

            context.SaveChanges();
        }
    }
}