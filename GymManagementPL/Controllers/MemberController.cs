using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.MemberViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService memberService1; 
        public MemberController(IMemberService memberService )
        {
            memberService1 = memberService; 
        }

         #region GetAllmembers 
        public ActionResult Index()
        {
          var members =  memberService1.GetAllMembers(); 

            return View(members);

        }
        #endregion



         #region  GetMemberData



        public ActionResult MemberDetails(int id)

        {

            if (id <= 0)

            {
                TempData["ErrorMessage"] = "Id of member is not zero or Nigative"; 
                return RedirectToAction(nameof(Index)); 
            }

            var member = memberService1.GetMemberDetails(id); 
            if (member == null )
            {
                TempData["ErrorMessage"] = "Member Not found ";
                return RedirectToAction(nameof(Index)); 
            }

            return View(member); 


        }




        #endregion




        #region HealthrecordDtails

        public ActionResult Healshrecorddetails (int id )
        {
            if (id <= 0)

            {
                TempData["Error message "] = "Id of Healthrecord is not zero or Nigative";

                return RedirectToAction(nameof(Index));
            }

            var Healthrecord = memberService1.GetMemberHealthRecordDetails(id);

            if (Healthrecord == null)
            {
                TempData["Error message "] = "Healthrecord Not found ";
                return RedirectToAction(nameof(Index));
            }

            return View(Healthrecord);




        }

        #endregion




        #region Add Member 


        public ActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult CreateMember ( CreateMemberViewModel createMember)
        {

            if(!ModelState.IsValid)
            {

                ModelState.AddModelError("DataInvalid", "Check data and missing fields");

                return View(nameof(Create), createMember); 
            }

            bool result = memberService1.CreateMember(createMember); 

            if (result)
            {
                TempData["SuccessMessage"] = "Member Created Successfully";

            }
            else
            {

                TempData["ErrorMessage"] = "Add member Failed";
            }


            return RedirectToAction(nameof(Index)); 





        }

        #endregion





        #region UpdateMember 


        public ActionResult MemberEdit(int id)
        {


            if (id <= 0)

            {
                TempData["ErrorMessage"] = "Id of member is not zero or Nigative";
                return RedirectToAction(nameof(Index));
            }


            var Member = memberService1.GetMemberToUpdate(id); 
            if(Member == null)
            {
                TempData["ErrorMessage"] = "Member Not found ";
                return RedirectToAction(nameof(Index));
            }

            return View(Member); 


        }


        [HttpPost]
        public ActionResult MemberEdit ([FromRoute] int id , MemberToUpdateViewModel memberToUpdate)
        {


            if (!ModelState.IsValid)
                return View(memberToUpdate);


            var result = memberService1.UpdateMmeberDetails(id, memberToUpdate);


            if (result)
            {
                TempData["SuccessMessage"] = "Member Updated Successfully";

            }
            else
            {

                TempData["ErrorMessage"] = "Update member Failed";
            }


            return RedirectToAction(nameof(Index));



        }





        #endregion




        #region DeleteMember



        public ActionResult Delete (int id )
        {

            if (id <= 0)

            {
                TempData["ErrorMessage"] = "Id of member is not zero or Nigative";
                return RedirectToAction(nameof(Index));
            }


            var Member = memberService1.GetMemberDetails(id);
            if (Member == null)
            {
                TempData["ErrorMessage"] = "Member Not found ";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id; 

            return View();

        }


        [HttpPost] 
        public ActionResult DeleteConfirm (int id)
        {
            var result = memberService1.RemoveMember(id);

            if (result)
            {
                TempData["SuccessMessage"] = "Member Deleted Successfully";

            }
            else
            {

                TempData["ErrorMessage"] = "Delete member Failed";
            }


            return RedirectToAction(nameof(Index));


        }



        #endregion 












    }
}
