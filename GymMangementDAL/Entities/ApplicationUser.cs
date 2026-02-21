using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangementDAL.Entities
{
    public class ApplicationUser : IdentityUser
    {

        public string FirsName { get; set; } = null!;
        public string LastNamee { get; set; } = null!; 





    }
}
