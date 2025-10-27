using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.TrainerViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {

        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }


        #region GetAll
        public ActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }
        #endregion 



        #region GetDetails
        public ActionResult TrainerDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Can't be 0 or Nigative Number";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerDetails(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }
        #endregion




        #region create



        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvaild", "Check Data and Missing Fields");
                return View(nameof(Create), createTrainer);
            }

            bool result = _trainerService.CreateTrainer(createTrainer);
            if (result)
                TempData["SuccessMessage"] = "Trainer Created Successfuly";
            else
                TempData["ErrorMessage"] = "Trainer Failed to Create, Check Phone and Email";

            return RedirectToAction(nameof(Index));
        }

        #endregion



        #region Edit

        public ActionResult TrainerEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Can't be 0 or Nigative Number";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerToUpdate(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }
        [HttpPost]
        public ActionResult TrainerEdit([FromRoute] int id, TrianerToUpdateViewModel editTrainer)
        {
            if (!ModelState.IsValid)
                return View(editTrainer);

            var result = _trainerService.UpdateTrainer(id, editTrainer);
            if (result)
                TempData["SuccessMessage"] = "Trainer Updated Successfuly";
            else
                TempData["ErrorMessage"] = "Trainer Failed to Update, Check Phone and Email";

            return RedirectToAction(nameof(Index));
        }


        #endregion





        #region Delete


        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id of Trainer Can't be 0 or Nigative Number";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerDetails(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerId = id;

            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            var result = _trainerService.RemoveTrainer(id);

            if (result)
                TempData["SuccessMessage"] = "Trainer Deleted Successfuly";
            else
                TempData["ErrorMessage"] = "Trainer Failed to Delete";

            return RedirectToAction(nameof(Index));
        }


        #endregion




    }
}
