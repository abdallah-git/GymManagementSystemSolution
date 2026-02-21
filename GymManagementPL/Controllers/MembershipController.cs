using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MembershipViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class MembershipController : Controller
    {
        private readonly MembershipService membershipService1; 
        public MembershipController( MembershipService  membershipService )
        {
            membershipService1 = membershipService; 
        }
        public IActionResult Index()
        {
            var memberships = membershipService1.GetAllMemberShips();

            return View(memberships); 
        }



        public IActionResult Create()
        {
            LoadDropdowns();
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(CreateMemberShipViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = membershipService1.CreateMemberShip(model);

                if (result)
                {
                    TempData["SuccessMessage"] = "Membership created successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Membership can not be created";
                    return RedirectToAction("Index");

                }

            }

            TempData["ErrorMessage"] = "Creation failed, Check the data";
            LoadDropdowns();
            return View(model);
        }


        public IActionResult Cancel (int Id )
        {

            var Result = membershipService1.DeleteMemberShip(Id); 
            if(Result)
            {
                TempData["SuccessMessage"] = "MemberShip Deleted Successfully";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "MemberShip Deleted Failed";
                return RedirectToAction(nameof(Index));

            }

        }







        #region Helper Method

        public void LoadDropdowns()
        {
            var members = membershipService1.GetMembersForDropDown();
            var plans = membershipService1.GetPlansForDropDown();

            ViewBag.Members = new SelectList(members, "Id", "Name");
            ViewBag.Plans = new SelectList(plans, "Id", "Name");

        }

        #endregion






    }
}
