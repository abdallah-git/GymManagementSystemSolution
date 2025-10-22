using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.Analyticsviewmodel;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class Analyticservice : IAnalyticservice
    {
        private readonly IUnitOfWork unitOfWork1;
        public Analyticservice(IUnitOfWork unitOfWork)
        {
            unitOfWork1 = unitOfWork;
        }



        AnalyticViewModel? IAnalyticservice.GetAnalyticsData()
        {



            return new AnalyticViewModel()
            {

                ActiveMembers = unitOfWork1.GetRepository<Membership>().GetAll(x => x.Statues == "Active").Count(),
                TotalMembers = unitOfWork1.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = unitOfWork1.GetRepository<Trainer>().GetAll().Count(),
                UpcomingSessions = unitOfWork1.sessionRepository.GetAll().Count(x => x.StartDate > DateTime.Now),
                OngoingSessions = unitOfWork1.sessionRepository.GetAll().Count(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now),
                CompletedSessions = unitOfWork1.sessionRepository.GetAll().Count(x => x.EndDate < DateTime.Now)

            };


        }


    }
}
