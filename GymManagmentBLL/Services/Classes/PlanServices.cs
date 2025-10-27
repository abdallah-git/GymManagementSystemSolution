using AutoMapper;
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
    public class PlanServices : IPlanService
    {

        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper mapper1; 
        public PlanServices(IUnitOfWork unitOfWork , IMapper mapper  ) 
        {
            _unitofWork = unitOfWork;
            mapper1 = mapper; 
        }


        public IEnumerable<PlanViewModel> GetAllPlans()



        {


            var Plans = _unitofWork.GetRepository<Plan>().GetAll();
            if (Plans == null || !Plans.Any()) return [];


            var plans = _unitofWork.GetRepository<Plan>().GetAll();
            return mapper1.Map<IEnumerable<PlanViewModel>>(Plans);



        }



        public PlanViewModel? GetPlanDetails(int planid)
        {

            var plan = _unitofWork.GetRepository<Plan>().GetById(planid);
            if (plan == null) return null;

            return mapper1.Map<PlanViewModel>(plan); 




        }





        public UpdatePlanViewModel? GetPlantoUpdate(int planid)
        {
            var plan = _unitofWork.GetRepository<Plan>().GetById(planid);
            if (plan is null || plan.IsActive  || HasAciveMembership(planid)) return null;


            return mapper1.Map<UpdatePlanViewModel>(plan);

        }




        public bool UpdatePlan(int planid, UpdatePlanViewModel updatePlan)
        {
            try
            {
                var plan = _unitofWork.GetRepository<Plan>().GetById(planid);

                if (plan is null || plan.IsActive || HasAciveMembership(planid)) return false;

                mapper1.Map(updatePlan, plan); 

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
