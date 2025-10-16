using AutoMapper;
using GymManagmentBLL.ViewModels.MemberViewModel;
using GymManagmentBLL.ViewModels.PlanViewModel;
using GymManagmentBLL.ViewModels.SesssionViewModel;
using GymMangementDAL.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {

            #region session mapping
            CreateMap<Session, SessionViewModel>()

                .ForMember(des => des.TrainerName, Options => Options.MapFrom(src => src.SessionTrainer.Name))
                 .ForMember(des => des.CategoryName, Options => Options.MapFrom(src => src.sessioncategory.CategoryName))
                 .ForMember(des => des.AvailableSlots, Options => Options.Ignore());


            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
            #endregion


            #region member mapping 
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    City = src.City,
                    Street = src.Street
                }))
                .ForMember(dest => dest.Healthrecord, opt => opt.MapFrom(src => new Healthrecord
                {
                    Hieght = src.HealthRecordViewModel.Height,
                    Weight = src.HealthRecordViewModel.Weight,
                    BloodType = src.HealthRecordViewModel.BloodType,
                    Note = src.HealthRecordViewModel.Note
                }));



            CreateMap<Member, MemberViewModel>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                $"{src.Address.City}, {src.Address.Street}, {src.Address.BuildingNumber}"));


            CreateMap<Healthrecord, HealthRecordViewModel>();

            CreateMap<Member, MemberToUpdateViewModel>() 
           .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
           .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
           .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street));


            CreateMap<MemberToUpdateViewModel, Member>()
                    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                        {
                           City = src.City,
                           Street = src.Street,
                           BuildingNumber = src.BuildingNumber
                        }))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());






            #endregion



            #region Plan mapping

            CreateMap<Plan, PlanViewModel>();

            CreateMap<Plan, UpdatePlanViewModel>().ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Name));





            #endregion







        }


    }
}
