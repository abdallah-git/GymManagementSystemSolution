using GymManagmentBLL.ViewModels.MembershipViewModel;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentBLL.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IMembershipService
    {

        IEnumerable<MembershipViewModel> GetAllMemberShips();


        IEnumerable<PlanForSelectListViewModel> GetPlansForDropDown();

        IEnumerable<MemberForSelectListViewModel> GetMembersForDropDown();


        bool DeleteMemberShip(int MemberId); 


        bool CreateMemberShip(CreateMemberShipViewModel model); 






    }
}
