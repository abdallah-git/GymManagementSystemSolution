using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.BookingViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class Booking(IBookingService bookingService) : Controller
    {
        public IActionResult Index()
        {
            var sessions = bookingService.GetAllSeesionsWithTrainerAndCategory();
            return View(sessions);
        }


        public IActionResult GetMembersForUpcomingSession(int id)
        {
            var members = bookingService.GetAllMembersForUpcomingSessions(id);
            return View(members);
        }

        public IActionResult GetMembersForOngoingSession(int id)
        {
            var members = bookingService.GetAllMembersForOngoingSessions(id);
            return View(members);
        }



        public IActionResult Create(int id)
        {
            var members = bookingService.GetMembersForDropdown(id);
            ViewBag.Members = new SelectList(members, "Id", "Name");
            return View();
        }




        [HttpPost]
        public IActionResult Create(CreateBookingViewModel model)
        {
            var result = bookingService.CreateBooking(model);
            if (result)
            {
                TempData["SuccessMessage"] = "Booking Created successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Create Booking.";
            }

            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = model.SessionId });
        }






    

        [HttpPost]
        public IActionResult Cancel(MemberAttendOrCancelViewModel model)
        {
            var result = bookingService.CancelBooking(model);

            if (result)
                TempData["SuccessMessage"] = "Booking cancelled successfully";
            else
                TempData["ErrorMessage"] = "Booking can't be cancelled";
            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = model.SessionId });
        }






    }
}
