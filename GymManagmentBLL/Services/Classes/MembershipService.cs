using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MembershipViewModel;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Classes;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork unitOfWork1;
        private readonly IMapper mapper1; 
        public MembershipService(IUnitOfWork unitOfWork , IMapper mapper   )
        {
            unitOfWork1 = unitOfWork;
            mapper1 = mapper;
        }

        public bool CreateMemberShip(CreateMemberShipViewModel model)
        {
            if (!IsMemberExists(model.MemberId) || !IsPlanExists(model.PlanId) || HasActiveMembership(model.MemberId))
                return false;

            var membershipRepo = unitOfWork1.GetRepository<Membership>();

            var membershipToCreate = mapper1.Map<Membership>(model);

            var plan = unitOfWork1.GetRepository<Plan>().GetById(model.PlanId);

            // BUSSINESS RULE #5: When a membership is created, its EndDate
            // is automatically calculated based on the plan duration.
            membershipToCreate.EndDate = DateTime.UtcNow.AddDays(plan!.DurationDayes);

            membershipRepo.Add(membershipToCreate);

            return unitOfWork1.Savechanges() > 0;

        }



        public IEnumerable<MembershipViewModel> GetAllMemberShips()
        {


            var memberships = unitOfWork1.MembershipRepository.GetAllMembershipsWithMembersAndPlans(m => m.Statues == "Active");
            var  membershipViewModels = mapper1.Map<IEnumerable<MembershipViewModel>>(memberships);

            return membershipViewModels; 


        }


        public IEnumerable<PlanForSelectListViewModel> GetPlansForDropDown()
        {
            var Plans = unitOfWork1.GetRepository<Plan>().GetAll(X => X.IsActive == true);
            return mapper1.Map<IEnumerable<PlanForSelectListViewModel>>(Plans);

        }

        public IEnumerable<MemberForSelectListViewModel> GetMembersForDropDown()
        {
            var Members = unitOfWork1.GetRepository<Member>().GetAll();
            return mapper1.Map<IEnumerable<MemberForSelectListViewModel>>(Members);
        }

        public bool DeleteMemberShip(int MemberId)
        {

            var membershipRepo = unitOfWork1.MembershipRepository;

            var membershipToDelete = membershipRepo.GetFirstOrDefault(m => m.MemberId == MemberId && m.Statues == "Active");

            if (membershipToDelete is null)
                return false;

            membershipRepo.Delete(membershipToDelete);
            return unitOfWork1.Savechanges() > 0;


        }



        #region Helper methods


        // BUSSINESS RULE #1: A membership can only be created if the member exists in the system
        private bool IsMemberExists(int memberId)
            => unitOfWork1.GetRepository<Member>().GetById(memberId) is not null;

        // BUSSINESS RULE #2: A membership can only be created if the plan exists in the system.
        private bool IsPlanExists(int planId)
            => unitOfWork1.GetRepository<Plan>().GetById(planId) is not null;
        // BUSSINESS RULE #3: A member cannot have more than one Active membership at the same time.
        private bool HasActiveMembership(int memberId)
        => unitOfWork1.MembershipRepository.GetAllMembershipsWithMembersAndPlans(m => m.Statues == "Active" && m.MemberId == memberId).Any();

        


        #endregion




    }
}
