using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.PlanViewModel;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Classes;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    internal class PlanServices : IPlanService
    {

        private readonly IUnitOfWork _unitofWork; 
        public PlanServices(IUnitOfWork unitOfWork ) 
        {
            _unitofWork = unitOfWork; 
        }


        public IEnumerable<PlanViewModel> GetAllPlans()
        {


            var Plans = _unitofWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any()) return [];


            return Plans.Select(p => new PlanViewModel
            {
                Name = p.Name,
                Description = p.Description,
                DurationDayes = p.DurationDayes,
                IsActive = p.IsActive,
                Price = p.Price ,
                Id = p.Id
            }); 



        }

        public PlanViewModel? GetPlanDetails(int planid)
        {

            var plan = _unitofWork.GetRepository<Plan>().GetById(planid);
            if (plan == null) return null;

            return new PlanViewModel()
            {

                Name = plan.Name , 
                Description = plan.Description , 
                Id = plan.Id , 
                IsActive = plan.IsActive,
                Price= plan.Price ,
                DurationDayes = plan.DurationDayes 
            }; 




        }





        public UpdatePlanViewModel? GetPlantoUpdate(int planid)
        {
            var plan = _unitofWork.GetRepository<Plan>().GetById(planid);
            if (plan is null || plan.IsActive == false || HasAciveMembership(planid)) return null;


            return new UpdatePlanViewModel()
            {

                Description = plan.Description , 
                DurationDayes = plan.DurationDayes , 
                PlanName = plan.Name , 
                Price = plan .Price 

            };


        }




        public bool UpdatePlan(int planid, UpdatePlanViewModel updatePlan)
        {
            try
            {
                var plan = _unitofWork.GetRepository<Plan>().GetById(planid);

                if (plan is null || HasAciveMembership(planid)) return false;

                plan.Description = updatePlan.Description;
                plan.Price = updatePlan.Price;
                plan.DurationDayes = updatePlan.DurationDayes;


                _unitofWork.GetRepository<Plan>().Update(plan);

                return _unitofWork.Savechanges() > 0;
            }
            catch
            {
                return false; 
            }


        }




        public bool Toogle(int PlanId)
        {


            var plan = _unitofWork.GetRepository<Plan>().GetById(PlanId);

            if (plan is null || HasAciveMembership(PlanId)) return false;


            plan.IsActive = plan.IsActive == true ? false : true;

            _unitofWork.GetRepository<Plan>().Update(plan);
            return _unitofWork.Savechanges() > 0; 

        }

      



        #region HelperMethods 


        private bool HasAciveMembership ( int planid )
        {
            return _unitofWork.GetRepository<Membership>().GetAll(x => x.PlanId == planid && x.Statues == "Active").Any(); 
        }


        #endregion 




    }
}
