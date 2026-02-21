using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels.BookingViewModel
{
    public class MemberForSessionViewModel
    {
        public int MemberId { get; set; }

        public string MemberName { get; set; } = null!;

        public int SessionId { get; set; }

        public bool IsAttended { get; set; }


        public string BookingDate { get; set; } = null!; 

       

    }
}
