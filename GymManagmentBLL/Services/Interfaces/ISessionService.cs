using GymManagmentBLL.ViewModels.SesssionViewModel;
using GymMangementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface ISessionService
    {


        IEnumerable<SessionViewModel> GetAllSessions();

        SessionViewModel? GetSessionById(int id);


        bool CreateSession(CreateSessionViewModel createSession);


        UpdateSessionViewModel? GetSessionToUpdate(int sessionid);


        bool UpdateSession(UpdateSessionViewModel updateSession, int sessionid);


        bool RemoveSession(int sessionid);


        IEnumerable<TrainerSelectviewModel> GetTrainersfordropdown();

        IEnumerable<Categoeryselectviewmodel> GetCategoeriesfordropdown();  



    }
}
