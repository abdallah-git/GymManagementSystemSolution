using GymManagmentBLL.ViewModels.BookingViewModel;
using GymManagmentBLL.ViewModels.MembershipViewModel;
using GymManagmentBLL.ViewModels.SesssionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IBookingService
    {

        IEnumerable<SessionViewModel> GetAllSeesionsWithTrainerAndCategory();


        IEnumerable<MemberForSessionViewModel> GetAllMembersForUpcomingSessions(int id);


        IEnumerable<MemberForSessionViewModel> GetAllMembersForOngoingSessions(int id);



        public bool CreateBooking(CreateBookingViewModel model);


        IEnumerable<MemberForSelectListViewModel> GetMembersForDropdown(int id);


       
        bool CancelBooking(MemberAttendOrCancelViewModel model);


    }
}
