using GymMangementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.DataSeed
{
    public static class IdentityDbcontextSeeding
    {


        public static bool SeedData (RoleManager<IdentityRole> roleManager , UserManager<ApplicationUser> userManager)
        {
            try
            {

                var HasUsers = userManager.Users.Any();
                var HasRoles = roleManager.Roles.Any();
                if (HasUsers && HasRoles) return false;

                if (!HasRoles) 
                {
                    var Roles = new List<IdentityRole>()
                  {
                      new() {Name = "SuperAdmin"} ,
                      new() {Name = "Admin"}

                  };

                    foreach (var Role in Roles)
                    {

                        if (!roleManager.RoleExistsAsync(Role.Name!).Result)
                        {
                            roleManager.CreateAsync(Role).Wait(); 
                        }

                    }




                }


                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {

                        FirsName = "Abdallah",
                        LastNamee = "Yaqout",
                        UserName = "AbdallahYaqout",
                        Email = "AbdallahYaqout@gmail.com",
                        PhoneNumber = "01129815414"

                    };



                    userManager.CreateAsync(MainAdmin,"P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();


                    var Admin = new ApplicationUser()
                    {

                        FirsName = "Ahmed",
                        LastNamee = "Omar",
                        UserName = "AhmedOmar",
                        Email = "AhmedOmar@gmail.com",
                        PhoneNumber = "01129815414"

                    };



                    userManager.CreateAsync(Admin, "P@ssw0rd").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();



                }


                return true; 



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seed Failed : {ex}");
                return false; 
            }
        }

       

    }
}
