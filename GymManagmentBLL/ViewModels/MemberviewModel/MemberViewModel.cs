using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.MemberviewModel
{
    internal class MemberViewModel
    {
        public int Id { get; set; }

        public String Name { get; set; } = null!;


        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!; 

        public string? Photo { get; set; }

        public String Gender { get; set; } = null!; 


        public string? PlanName { get; set; } 


        public string? DateOfbirth { get; set; }


        public string? MembershipStartDate { get; set; }

        public string? MembershipEndDate { get; set; } 

        public string? Address { get; set; } 






    }
}
