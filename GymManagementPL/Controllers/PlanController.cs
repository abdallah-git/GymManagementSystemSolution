using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.PlanViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {

        private readonly IPlanService planService1; 
        public PlanController(IPlanService planService )
        {
            planService1 = planService; 
        }


        public IActionResult Index()
        {
            var plans = planService1.GetAllPlans();

            return View(plans); 
        }


        public ActionResult Details (int id )
        {
            if (id <=0 )
            {
                TempData["ErrorMessage"] = "Plan id not found";
                return RedirectToAction(nameof(Index)); 
            }

            var plan = planService1.GetPlanDetails(id); 

            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan found";
                return RedirectToAction(nameof(Index));


            }

            return View(plan); 
        }



        public ActionResult Edit (int id )
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Plan id not found";
                return RedirectToAction(nameof(Index));
            }

            var plan = planService1.GetPlantoUpdate(id);

            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan Not found";
                return RedirectToAction(nameof(Index));


            }

            return View(plan);
        }


        [HttpPost]
        public ActionResult Edit (int id ,UpdatePlanViewModel updatePlan   )
        {

            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("WrongData", "Check data validation ");
                return View(nameof(Edit), updatePlan); 
               

            }

            var result = planService1.UpdatePlan(id, updatePlan); 
            if( result)
            {
                TempData["SuccessMessage"] = "Plan Updated";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Update failed";
            }
            return RedirectToAction(nameof(Index)); 
                  


        }


        [HttpPost]
        public ActionResult Activate (int id )
        {
            var result = planService1.Toogle(id); 

            if ( result)
            {
                TempData["SuccessMessage"] = "Plan status changed";

            }
            else
            {
                TempData["ErrorMessage"] = "Plan status failed";

            }

            return RedirectToAction(nameof(Index));



        }

       



    }




}

