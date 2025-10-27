using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels.SesssionViewModel;
using GymMangementDAL.Entities;
using GymMangementDAL.Repositories.Classes;
using GymMangementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class SessionService : ISessionService
    {

        private readonly IUnitOfWork unitOfWork1;
        private readonly IMapper mapper1; 


        public SessionService(IUnitOfWork unitOfWork , IMapper mapper )
        {
            unitOfWork1 = unitOfWork;
            mapper1 = mapper; 
            
        }

       

        public IEnumerable<SessionViewModel> GetAllSessions()

        {


            var sessions = unitOfWork1.sessionRepository.GetAllsesionswithtrainersandcategories(); 
            if (sessions is null ||! sessions.Any()) return [];


            var mappedsessions = mapper1.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            return mappedsessions; 
           




        }

        public SessionViewModel? GetSessionById (int id) 
        {

            var session = unitOfWork1.sessionRepository.GetSessionByIDwithtrainersandcategories(id); 

            if (session is null) return null;



            #region manualmapping
            //return new SessionViewModel()
            //{

            //   Capacity = session .Capacity , 
            //   Description = session.Descreption , 
            //   EndDate = session.EndDate , 
            //   StartDate = session.StartDate , 
            //   CategoryName = session.sessioncategory.CategoryName, 
            //   TrainerName = session.SessionTrainer.Name , 
            //   AvailableSlots =  session.Capacity -  unitOfWork1.sessionRepository.GetCountofbookslots(session.Id) 




            //}; 
            #endregion 





            var mappedsessions = mapper1.Map <Session,SessionViewModel>(session);
            return mappedsessions;









        }



        public bool CreateSession(CreateSessionViewModel createSession)
        {


            if (!IsTrainerexists(createSession.TrainerId)) return false;

            if (!IsCategoryexists(createSession.CategoryId)) return false;

            if (!IsValidDateRange(createSession.StartDate, createSession.EndDate)) return false;


            var Mapped = mapper1.Map<CreateSessionViewModel, Session>(createSession);

            unitOfWork1.sessionRepository.Add(Mapped);

            return unitOfWork1.Savechanges() > 0; 






        }




        public UpdateSessionViewModel? GetSessionToUpdate(int sessionid)
        {
            var session = unitOfWork1.sessionRepository.GetById(sessionid);

            if (!IsSessionAvaliableForUpdating(session)) return null;

            return mapper1.Map<Session, UpdateSessionViewModel>(session); 




        }

      
        public bool UpdateSession(UpdateSessionViewModel updateSession, int sessionid)
        {
            try
            {

                var session = unitOfWork1.sessionRepository.GetById(sessionid);
                if (!IsSessionAvaliableForUpdating(session)) return false;

                if (!IsTrainerexists(updateSession.TrainerId)) return false;

                if (!IsValidDateRange(updateSession.StartDate, updateSession.EndDate)) return false;

                mapper1.Map(updateSession, session);

                session.UpdatedAt = DateTime.Now;

                return unitOfWork1.Savechanges() > 0;
            }
            catch
            {
                return false; 
            }

        

       
        }




        public bool RemoveSession(int sessionid)
        {
            try
            {
                var session = unitOfWork1.sessionRepository.GetById(sessionid);
                if (!IsSessionAvaliableForDeleting(session)) return false;


                unitOfWork1.sessionRepository.Delete(session);
                return unitOfWork1.Savechanges() > 0;
            }
            catch
            {
                return false; 
            }

         
        }




        public IEnumerable<TrainerSelectviewModel> GetTrainersfordropdown()
        {
            var trainers = unitOfWork1.GetRepository<Trainer>().GetAll();


            return mapper1.Map<IEnumerable<Trainer>, IEnumerable<TrainerSelectviewModel>>(trainers); 
        }

        public IEnumerable<Categoeryselectviewmodel> GetCategoeriesfordropdown()
        {
            var categories = unitOfWork1.GetRepository<Category>().GetAll();


            return mapper1.Map<IEnumerable<Category>, IEnumerable<Categoeryselectviewmodel>>(categories);
        }








        #region HelperMethods 



        private bool IsTrainerexists (int Trainerid)
        {
            return unitOfWork1.GetRepository<Trainer>().GetById(Trainerid) is not null; 
        }

        private bool IsCategoryexists(int categoryid)
        {
            return unitOfWork1.GetRepository<Category>().GetById(categoryid) is not null;
        }

        private bool IsValidDateRange (DateTime StartDate , DateTime EndDate)
        {
            return StartDate < EndDate && StartDate > DateTime.Now; 
        }


        private bool IsSessionAvaliableForUpdating (Session session)
        {
            if (session == null) return false;

            if (session.StartDate <= DateTime.Now) return false;

            if (session.EndDate < DateTime.Now) return false;

            var HasactiveBooking = unitOfWork1.sessionRepository.GetCountofbookslots(session.Id) > 0;
            if (HasactiveBooking) return false;

            return true; 
        }

        private bool IsSessionAvaliableForDeleting(Session session)
        {
            if (session == null) return false;

            if (session.StartDate <= DateTime.Now && session.EndDate>DateTime.Now) return false;

            if (session.StartDate > DateTime.Now) return false;

            var HasactiveBooking = unitOfWork1.sessionRepository.GetCountofbookslots(session.Id) > 0;
            if (HasactiveBooking) return false;

            return true;
        }

     



        #endregion











    }
}
