using GymManagmentBLL.Services.Interfaces;
using GymMangementDAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {

        private readonly IAnalyticservice analyticservice1; 
        public HomeController(IAnalyticservice analyticservice )
        {
            analyticservice1 = analyticservice; 
        }


        public ActionResult Index()
        {
            var data = analyticservice1.GetAnalyticsData(); 

            return View(data);


        }







        #region Action Return types 
        //public ActionResult ViewResult ()
        //{
        //    return View(); 
        //}

        //public ActionResult Trainers()
        //{

        //    var Trainers = new List<Trainer>()
        //    {
        //        new Trainer () {Name = "Ahmed",Phone = "01129815414"} ,
        //       new Trainer () {Name = "Aya" , Phone = "01129815414"}
        //    };


        //    return Json(Trainers); 
        //}



        //public ActionResult Content()
        //{
        //    return Content(" <h1>Hello gym mangement system </h1> , text/html"); 
        //}



        //public ActionResult EmptyResult ()
        //{
        //    return EmptyResult(); 
        //}



        //public ActionResult Redirect ()
        //{
        //    return Redirect("https://www.netflix.com/eg-en/"); 
        //}


        #endregion 







    }
}
