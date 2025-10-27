using GymManagmentBLL.Services.Classes;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.SesssionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {

        private readonly ISessionService sessionService1;
        public SessionController(ISessionService sessionService)
        {
            sessionService1 = sessionService;
        }


        public IActionResult Index()
        {

            var sessions = sessionService1.GetAllSessions();
            return View(sessions);
        }



        public ActionResult Details(int id)

        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "session id not found";
                return RedirectToAction(nameof(Index));
            }

            var session = sessionService1.GetSessionById(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "session not  found";
                return RedirectToAction(nameof(Index));

            }

            return View(session);


        }




        public ActionResult Create()
        {

            LoadtraniersDropDown();
            LoadDcatigoriesropDown();

            return View();
        }


        [HttpPost]
        public ActionResult Create(CreateSessionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadtraniersDropDown();
                LoadDcatigoriesropDown(); 
                return View(viewModel);
            }

            var result = sessionService1.CreateSession(viewModel);

            if (result)
            {
                TempData["SuccessMessage"] = "session Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Add session Failed";
            }


            return RedirectToAction(nameof(Index));



        }


        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "session id not found";
                return RedirectToAction(nameof(Index));
            }

            var session = sessionService1.GetSessionToUpdate(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "session Not found";
                return RedirectToAction(nameof(Index));

            }
            LoadtraniersDropDown(); 

            return View(session);

        }

        [HttpPost]
        public ActionResult Edit([FromRoute] int id, UpdateSessionViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                LoadtraniersDropDown();
                
                return View(viewModel);
            }

            var result = sessionService1.UpdateSession(viewModel, id); 

            if(result)
            {
                TempData["SuccessMessage"] = "session updated Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "session Edit failed";

            }

            return RedirectToAction(nameof(Index)); 

        }

            

        public ActionResult Delete (int id )
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "session id not found";
                return RedirectToAction(nameof(Index));
            }

            var session = sessionService1.GetSessionById(id);

            if (session == null)
            {
                TempData["ErrorMessage"] = "session not  found";
                return RedirectToAction(nameof(Index));

            }
            ViewBag.Id = id; 
            return View();

        }


        [HttpPost]
        public ActionResult DeleteConfirmed(int id )
        {

            var result = sessionService1.RemoveSession(id); 

            if ( result )
            {



                TempData["SuccessMessage"] = "session Deleted Successfully";

            }
            else
            {

                TempData["ErrorMessage"] = "Delete session Failed";
            }


            return RedirectToAction(nameof(Index));

        }

        




       



        #region helper method  for create action  to load dropdown 


        private void LoadtraniersDropDown()
        {
            

            var trainers = sessionService1.GetTrainersfordropdown();
            ViewBag.trainers = new SelectList(trainers, "Id", "Name");
        }



        private void LoadDcatigoriesropDown()
        {
            var catigories = sessionService1.GetCategoeriesfordropdown();
            ViewBag.catigories = new SelectList(catigories, "Id", "Name");

           
        }

        #endregion







    }
}
