using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels;
using GymManagmentBLL.ViewModels.MemberviewModel;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Classes;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    internal class MemberService : IMemberservice
    {

        private readonly IUnitOfWork unitOfWork1; 
        public MemberService(IUnitOfWork unitOfWork)
        {
            unitOfWork1 = unitOfWork;

        }

        

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = unitOfWork1.GetRepository<Member>().GetAll();
            if (Members == null || !Members.Any()) return [];


            // member to memberviewmodel => mapping 
            #region way1 

            //var MeberViewModels = new List<MemberViewModel>(); 

            //foreach ( var member in Members )
            //{
            //    var memberviewmodel = new MemberViewModel()
            //    {
            //        Id = member.Id , 
            //        Name = member.Name,
            //        Phone = member.Phone,
            //        Photo = member.photo,
            //        Emali = member.Email,
            //        Gender = member.Gender.ToString()
            //    };

            //    MeberViewModels.Add(memberviewmodel); 

            //}
            //return MeberViewModels;  






            #endregion

            #region way2



            var MemberViewModels = Members.Select(x => new MemberViewModel
            {
                Id = x.Id , 
                Name = x.Name , 
                Email = x.Email , 
                Phone = x.Phone , 
                Photo = x.photo , 
                Gender = x.Gender.ToString() ,
            });

            return MemberViewModels; 

            #endregion 

        }


        public bool CreateMember(CreateMemberViewModel createMember)
        {


            if (IsEmailexist(createMember.Email) || IsPhoneexist(createMember.Phone)) return false; 

            else
            {


                var member = new Member
                {
                    Phone = createMember.Phone,
                    Email = createMember.Email,
                    Gender = createMember.Gender,
                    Name = createMember.Name,
                    DateOfBirth = createMember.DateOfBirth,
                    Address = new Address
                    {
                        BuildingNumber = createMember.Buildingnumber,
                        Street = createMember.Street,
                        City = createMember.City,
                    },
                    Healthrecord = new Healthrecord
                    {
                        BloodType = createMember.healthrecordviewmodel.Bloodtype,
                        Weight = createMember.healthrecordviewmodel.Weight,
                        Hieght = createMember.healthrecordviewmodel.Hieght,

                    }







                };

                unitOfWork1.GetRepository<Member>().Add(member) ;
               return unitOfWork1.Savechanges()> 0 ; 


            }
        }

        public MemberViewModel? GetMemberDetail(int memberId)
        {
            var member = unitOfWork1.GetRepository<Member>().GetById(memberId);
            if (member == null) return null;

            var viewModel = new MemberViewModel
            {

                Name = member.Name , 
                Email = member.Email , 
                Phone = member.Phone , 
                Photo = member.photo , 
                Gender = member.Gender.ToString() , 
                DateOfbirth= member.DateOfBirth.ToShortDateString() , 
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City} " ,

            };

            var ActiveMeberShip = unitOfWork1.GetRepository<Membership>()
                .GetAll(x => x.MemberId == memberId && x.Statues=="Active") .FirstOrDefault();
            if (ActiveMeberShip is not null)
            {
                viewModel.MembershipStartDate = ActiveMeberShip.CreatedAt.ToShortDateString();
                viewModel.MembershipEndDate = ActiveMeberShip.EndDate.ToShortDateString();
                var plan = unitOfWork1.GetRepository<Plan>().GetById(ActiveMeberShip.PlanId);
                viewModel.PlanName = plan?.Name; 

            }
            return viewModel; 



        }

        public Healthrecordviewmodel? GetMemberHealthRecord(int memberid)
        {

            var Healthrecordviewmodel = unitOfWork1.GetRepository<Healthrecord>()
                .GetById(memberid);
            if (Healthrecordviewmodel == null) return null;


            return new Healthrecordviewmodel
            {
                Bloodtype = Healthrecordviewmodel.BloodType,
                Note = Healthrecordviewmodel.Note,
                Weight = Healthrecordviewmodel.Weight,
                Hieght = Healthrecordviewmodel.Hieght,

            }; 
             




        }

        public MembertoUpdateViewModel GetMembertoUpdate(int memberid)
        {
            var member = unitOfWork1.GetRepository<Member>().GetById(memberid);
            if (member == null) return null;

            return new MembertoUpdateViewModel
            {
                Phone = member.Phone,
                Photo = member.photo,
                Name = member.Name,
                Buildingnumber = member.Address.BuildingNumber,
                City = member.Address.City,
                Street = member.Address.Street,
                Email = member.Email,
            }; 

        }

        public bool UpadateMmeberDetails(int memberid, MembertoUpdateViewModel membertoUpdate)
        {
            try
            {
                if (IsEmailexist(membertoUpdate.Email) || IsPhoneexist(membertoUpdate.Phone)) return false;
                var memberrepo = unitOfWork1.GetRepository<Member>(); 
                var member = memberrepo.GetById(memberid);
                if (member == null) return false;

                member.Email = membertoUpdate.Email;
                member.Phone = membertoUpdate.Phone; 
                member.Address.City = membertoUpdate.City;
                member.Address.Street = membertoUpdate.Street;
                member.Address.BuildingNumber = membertoUpdate.Buildingnumber;
                member.UpdatedAt = DateTime.Now;

                memberrepo.Update(member) ;
                return unitOfWork1.Savechanges() > 0; 


            }
            catch
            {
                return false; 
            }
        }

        #region HelperMethod 

        private bool IsEmailexist (string Email)
        {
            return unitOfWork1.GetRepository<Member>().GetAll(x => x.Email == Email).Any(); 
        }

        private bool IsPhoneexist(string phone)
        {
            return unitOfWork1.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        }







        #endregion



        public bool RemoveMember(int memberid)
        {
            try
            {


                var Member = unitOfWork1.GetRepository<Member>().GetById(memberid);
                if (Member == null) return false;



                var HasactiveMemberSessions = unitOfWork1.GetRepository<Membersession>() 
                   . GetAll(x => x.MemberId == memberid && x.Session.StartDate > DateTime.Now).Any();

                if (HasactiveMemberSessions) return false;


                var membershiprepo = unitOfWork1.GetRepository<Membership>(); 


                var memberships = membershiprepo.GetAll(x => x.MemberId == memberid);

                if (memberships.Any())
                {
                    foreach (var item in memberships)
                    {
                        membershiprepo.Delete(item); 
                    }
                }

                unitOfWork1.GetRepository<Member>().Delete(Member);
                return unitOfWork1.Savechanges() > 0; 


            }
            catch
            {
                return false; 
            }
        }











    }
}
