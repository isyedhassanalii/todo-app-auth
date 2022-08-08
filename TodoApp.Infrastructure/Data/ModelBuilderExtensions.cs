using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Core.DBEntities.Authentication;

namespace TodoApp.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Roles
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole { Id="567b0fc9-31c1-4f3b-811d-3b10413c4596", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id="421796b5-e5d9-474c-8a3d-7b667dac179b", Name = "User", NormalizedName = "USER" }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
            //////
            ///

            //ADMINISTRATOR:

            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();

            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email="admin@admin.com",
                    PasswordHash = hasher.HashPassword(null, "admin123")
                }
            );


            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "567b0fc9-31c1-4f3b-811d-3b10413c4596",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );
        }
    }
}
