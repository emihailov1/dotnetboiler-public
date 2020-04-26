using BusinessLogic.Models;
using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public static class DbInitializer
    {
        public static void Initialize(ApiDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            string defaultPassword = "P@ssw0rd";
            SecurityService securityService = new SecurityService();


            var users = new User[]
                {
                new User{FirstName="Admin",LastName="Admin",Email="admin@gmail.com",Role= Enums.Role.Administrator,Status=Enums.Status.Active, Password = securityService.HashPassword(defaultPassword),Created = DateTime.Now},
                new User{FirstName="User",LastName="User",Email="user@gmail.com",Role= Enums.Role.User,Status=Enums.Status.Active, Password = securityService.HashPassword(defaultPassword),Created = DateTime.Now}
                };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();

        }
    }
}
