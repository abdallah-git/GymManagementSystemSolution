using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentBLL.ViewModels.PlanViewModel;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper1; 


        //GenericReposatiry<Member> MemberReposatiry = new GenericReposatiry<Member>() ; 

        public MemberService(IUnitOfWork unitOfWork , IMapper mapper)

        {
            _unitOfWork = unitOfWork;
            mapper1 = mapper; 
        }



        public bool CreateMember(CreateMemberViewModel createMember)
        {
            try

            {

                if (IsEmailExist(createMember.Email) || IsPhoneExist(createMember.Phone)) return false;

                // CreateMemberViewModel - Member maaping

               

                var member = mapper1.Map<CreateMemberViewModel,Member>(createMember);

                _unitOfWork.GetRepository<Member>().Add(member);
                return _unitOfWork.Savechanges() > 0;

            }

            catch 
            {
                return false; 

            }

        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll(); 
            if (!members.Any() || members == null) return [];

            // member - memberviewmodel mapping
            //var memberviewmodels = new List<MemberViewModel>();
            #region way01
            //foreach (var member in members)
            //{
            //    var memberviewmodel = new MemberViewModel()
            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Phone = member.Phone,
            //        Email = member.Email,
            //        Photo = member.Photo,
            //        Gender = member.Gender.ToString(),


            //    };
            //    memberviewmodels.Add(memberviewmodel);

            //} 
            #endregion

            var memberViewModels = mapper1.Map<IEnumerable<MemberViewModel>>(members);
            return memberViewModels;

            //return members;

        }

        public MemberViewModel? GetMemberDetails(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId); 
            if (member == null) return null;

            // member - memberviewmodel
            var viewmodel = mapper1.Map<Member, MemberViewModel>(member); 
            
            var Activemembership = _unitOfWork.GetRepository<Membership>()
                                .GetAll(X => X.MemberId == memberId).FirstOrDefault();

            if (Activemembership is not null)

            {
                viewmodel.MemberShipStartDate = Activemembership.CreatedAt.ToShortDateString();
                viewmodel.MemberShipEndDate = Activemembership.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetById(Activemembership.PlanId);
                viewmodel.PlanName = plan?.Name;
            }
            return viewmodel;
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int memberId)
        {
            var memberhealthrecored = _unitOfWork.GetRepository<Healthrecord>().GetById(memberId); 

            if (memberhealthrecored == null) return null;

            // healthrecord - healthrecordviewmodel => mappping

            var viewModel = mapper1.Map<HealthRecordViewModel>(memberhealthrecored);

            return viewModel;
        }


        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId); 
            if (member == null) return null;

          

            return mapper1.Map<MemberToUpdateViewModel>(member);


        }



        public bool UpdateMmeberDetails(int Id, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {
              var Emailexist  =   _unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Email == memberToUpdate.Email && x.Id != Id).Any();


                var Phonexist = _unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Email == memberToUpdate.Phone && x.Id != Id).Any();


                if (Emailexist || Phonexist) return false; 

                var MemberRepo = _unitOfWork.GetRepository<Member>(); 
                var member = MemberRepo.GetById(Id);
                if (member == null) return false;

                mapper1.Map(memberToUpdate, member);
                member.UpdatedAt = DateTime.Now;

                MemberRepo.Update(member);
                return _unitOfWork.Savechanges() > 0;

            }

            catch
            {
                return false;
            }


        }




        public bool RemoveMember(int id)
        {
            try
            {
                var memberrepo = _unitOfWork.GetRepository<Member>(); 
                var member = memberrepo.GetById(id);
                if (member == null) return false;

                var SessionsID = _unitOfWork.GetRepository<Membersession>()
                    .GetAll(X => X.MemberId == id).Select(x => x.SessionId);

                var HasActiveMmeberSessions = _unitOfWork.GetRepository<Session>()
                    .GetAll(x => SessionsID.Contains(x.Id) && x.StartDate > DateTime.Now).Any();

                if (HasActiveMmeberSessions) return false;

                var Memberships = _unitOfWork.GetRepository<Membership>().GetAll(X => X.MemberId == id);
                if (Memberships.Any())

                {
                    foreach (var membership in Memberships)
                    {
                        _unitOfWork.GetRepository<Membership>().Delete(membership); 

                    }

                }

                memberrepo.Delete(member);
                return _unitOfWork.Savechanges() > 0; 
            }
            catch
            {
                return false;
            }

        }



        #region Helper Methods

        private bool IsEmailExist(string email)

        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email).Any(); 
        }

        private bool IsPhoneExist(string phone)

        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any(); 
        }

        #endregion



    }
}
