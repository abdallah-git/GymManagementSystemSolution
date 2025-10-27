using GymManagmentBLL.ViewModels.PlanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IPlanService
    {

        IEnumerable<PlanViewModel> GetAllPlans();

        PlanViewModel? GetPlanDetails(int planid);


        UpdatePlanViewModel? GetPlantoUpdate(int planid);


        bool UpdatePlan(int planid, UpdatePlanViewModel updatePlan);


        bool Toogle(int PlanId); 



    }
}
