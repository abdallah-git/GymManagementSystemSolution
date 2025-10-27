using GymManagmentBLL.ViewModels.MemberViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IMemberService
    {


        IEnumerable<MemberViewModel> GetAllMembers();
        bool CreateMember(CreateMemberViewModel createMember);
        MemberViewModel? GetMemberDetails(int memberId);
        HealthRecordViewModel? GetMemberHealthRecordDetails(int memberId);
        MemberToUpdateViewModel? GetMemberToUpdate(int memberId);
        bool UpdateMmeberDetails(int Id, MemberToUpdateViewModel memberToUpdate);
        bool RemoveMember(int id);




    }
}
