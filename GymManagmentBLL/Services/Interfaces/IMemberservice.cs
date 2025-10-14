using GymManagmentBLL.ViewModels;
using GymManagmentBLL.ViewModels.MemberviewModel;
using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    internal interface IMemberservice
    {

        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMember);


        MemberViewModel? GetMemberDetail(int memberId);

        Healthrecordviewmodel? GetMemberHealthRecord(int memberid);



        MembertoUpdateViewModel GetMembertoUpdate(int memberid);



        bool UpadateMmeberDetails(int memberid, MembertoUpdateViewModel membertoUpdate);



        bool RemoveMember(int memberid); 


    }
}
